# 🧪 ASP.NET Core Web API Exercise

## ✨ Objective

In this exercise, you'll build a basic Web API in ASP.NET Core that includes middleware and a controller with two endpoints: one for retrieving file counts in a folder, and one for renaming a folder.

This exercise is designed to help you understand and apply core concepts of:

- Middleware
- HTTP headers
- Request validation
- File system access
- API routing and controller design

---

## 🧱 Project Structure

You will create a new blank solution and add a single Web API project inside it. The application will expose two endpoints (GET and POST) and will include a custom middleware component to validate requests.

---

## 🧩 Step-by-Step Instructions

### 1. Create a Blank Solution

- Create a new empty solution named `AspNetCourseFileManager` (or any name of your choice).

### 2. Add a Web API Project

- Add a new ASP.NET Core Web API project to the solution.
- Do **not** enable HTTPS or authentication for simplicity.

### 3. Implement Custom Middleware

Create a middleware that:

- Checks for the presence of a **header** named: `aspnetcourse`
- Validates that the value of the header is exactly: `06072025`
- If the header is missing or the value is incorrect, the middleware should:
  - Return HTTP status code `401 Unauthorized`
  - Stop the pipeline from continuing

The middleware should be globally applied to all requests.

---

## 🧭 API Endpoints

Create a single controller with two endpoints:

---

### **GET** `/filemanager`

**Purpose:**  
Count how many files are in a specified folder on the server.

**Request:**  
- Accept a query parameter named `folderName`.

**Behavior:**
- If the folder exists:
  - Return the number of files in the folder (non-recursive).
- If the folder does not exist:
  - Return a suitable error code and message (404)

---

### **POST** `/filemanager`

**Purpose:**  
Rename a folder on the server.

**Request Body:**  
- JSON object with two properties:
  - `oldName`: the current folder name
  - `newName`: the desired new folder name

**Behavior:**
- If the `oldName` folder exists:
  - Rename it to `newName`
  - Return success (201 or 200)
- If `oldName` does not exist or `newName` is already taken:
  - Return an appropriate error message

---

## 🧪 Testing the API

Use tools like **Postman**  or your browser (for the GET) to test the API.

**Important:** Every request must include the correct `aspnetcourse` header, or it will be rejected by the middleware.

---

## ✅ Completion Checklist

Make sure the following items are working before submitting:

- [ ] Solution and project are created correctly
- [ ] Middleware correctly validates the header and returns 401 when needed
- [ ] GET endpoint returns the correct file count for an existing folder
- [ ] POST endpoint renames a folder if possible and handles errors correctly
- [ ] Appropriate status codes are returned for all scenarios
- [ ] Clean, readable code with comments where necessary
