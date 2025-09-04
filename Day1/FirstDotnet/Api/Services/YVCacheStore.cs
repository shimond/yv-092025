using Microsoft.AspNetCore.OutputCaching;
using System.Text.Json;
using System.Collections.Concurrent;

namespace Api.Services
{
    public class YVJsonFileCacheStore : IOutputCacheStore
    {
        private readonly ILogger<YVJsonFileCacheStore> _logger;
        private readonly string _cacheDirectory;
        private readonly string _cacheFilePath;
        private readonly ConcurrentDictionary<string, CacheEntry> _memoryCache;
        private readonly SemaphoreSlim _fileLock;

        public YVJsonFileCacheStore(ILogger<YVJsonFileCacheStore> logger)
        {
            _logger = logger;
            _cacheDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Cache");
            _cacheFilePath = Path.Combine(_cacheDirectory, "cache.json");
            _memoryCache = new ConcurrentDictionary<string, CacheEntry>();
            _fileLock = new SemaphoreSlim(1, 1);

            // Ensure cache directory exists
            Directory.CreateDirectory(_cacheDirectory);

            // Load existing cache from file
            LoadCacheFromFile();
        }

        public async ValueTask<byte[]?> GetAsync(string key, CancellationToken cancellationToken)
        {
            _logger.LogDebug("Getting cache entry for key: {Key}", key);
                
            if (_memoryCache.TryGetValue(key, out var entry))
            {
                // Check if cache entry is still valid
                if (entry.ExpiresAt > DateTime.UtcNow)
                {
                    _logger.LogDebug("Cache hit for key: {Key}", key);
                    return entry.Data;
                }
                else
                {
                    // Remove expired entry
                    _logger.LogDebug("Cache entry expired for key: {Key}, removing from cache", key);
                    _memoryCache.TryRemove(key, out _);
                    await SaveCacheToFileAsync();
                    return null;
                }
            }

            _logger.LogDebug("Cache miss for key: {Key}", key);
            return null;
        }

        public async ValueTask SetAsync(string key, byte[] value, string[]? tags, TimeSpan validFor, CancellationToken cancellationToken)
        {
            var expiresAt = DateTime.UtcNow.Add(validFor);
            var cacheEntry = new CacheEntry
            {
                Data = value,
                Tags = tags ?? Array.Empty<string>(),
                ExpiresAt = expiresAt,
                CreatedAt = DateTime.UtcNow
            };

            _memoryCache.AddOrUpdate(key, cacheEntry, (k, v) => cacheEntry);
            
            // Save to file asynchronously
            await SaveCacheToFileAsync();
        }

        public async ValueTask EvictByTagAsync(string tag, CancellationToken cancellationToken)
        {
            var keysToRemove = new List<string>();

            foreach (var kvp in _memoryCache)
            {
                if (kvp.Value.Tags.Contains(tag, StringComparer.OrdinalIgnoreCase))
                {
                    keysToRemove.Add(kvp.Key);
                }
            }

            foreach (var key in keysToRemove)
            {
                _memoryCache.TryRemove(key, out _);
            }

            if (keysToRemove.Count > 0)
            {
                await SaveCacheToFileAsync();
            }
        }

        private void LoadCacheFromFile()
        {
            try
            {
                if (File.Exists(_cacheFilePath))
                {
                    var json = File.ReadAllText(_cacheFilePath);
                    if (!string.IsNullOrWhiteSpace(json))
                    {
                        var cacheData = JsonSerializer.Deserialize<Dictionary<string, CacheEntry>>(json);
                        if (cacheData != null)
                        {
                            var currentTime = DateTime.UtcNow;
                            foreach (var kvp in cacheData)
                            {
                                // Only load non-expired entries
                                if (kvp.Value.ExpiresAt > currentTime)
                                {
                                    _memoryCache.TryAdd(kvp.Key, kvp.Value);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log error in a real application
                Console.WriteLine($"Error loading cache from file: {ex.Message}");
            }
        }

        private async Task SaveCacheToFileAsync()
        {
            await _fileLock.WaitAsync();
            try
            {
                // Remove expired entries before saving
                var currentTime = DateTime.UtcNow;
                var validEntries = _memoryCache
                    .Where(kvp => kvp.Value.ExpiresAt > currentTime)
                    .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

                // Update memory cache to remove expired entries
                var expiredKeys = _memoryCache.Keys.Except(validEntries.Keys).ToList();
                foreach (var expiredKey in expiredKeys)
                {
                    _memoryCache.TryRemove(expiredKey, out _);
                }

                var json = JsonSerializer.Serialize(validEntries, new JsonSerializerOptions
                {
                    WriteIndented = true,
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                });

                await File.WriteAllTextAsync(_cacheFilePath, json);
            }
            catch (Exception ex)
            {
                // Log error in a real application
                Console.WriteLine($"Error saving cache to file: {ex.Message}");
            }
            finally
            {
                _fileLock.Release();
            }
        }

        public void Dispose()
        {
            _fileLock?.Dispose();
        }
    }

    public class CacheEntry
    {
        public byte[] Data { get; set; } = Array.Empty<byte>();
        public string[] Tags { get; set; } = Array.Empty<string>();
        public DateTime ExpiresAt { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
