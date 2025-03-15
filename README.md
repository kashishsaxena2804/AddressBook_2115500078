# Address Book ASP.NET Core API

## 📌 Architecture Overview

### Presentation Layer (API)
- Controllers for AddressBook and User Authentication
- Uses Swagger for API documentation

### Business Logic Layer (Services)
- Manages business logic for AddressBook and User Authentication
- Handles password hashing, JWT generation, and email sending

### Data Access Layer (Repositories)
- Communicates with MS SQL using Entity Framework Core
- Implements Repository Pattern for database operations

### Security & Communication
- JWT Authentication
- SMTP for email verification & password reset
- RabbitMQ for event-driven communication
- Redis for caching and session management
- Hashing (Salting) for password encryption

## 📌 Address Book ASP.NET Core API - Use Cases

### 🔹 Architecture
- **Presentation Layer (API Controllers)**
  - Exposes RESTful API endpoints
  - Uses Swagger for API documentation
  - Implements JWT-based authentication
- **Business Logic Layer (Services)**
  - Handles user authentication, password management, email sending
  - Implements CRUD operations for address book entries
- **Data Access Layer (Repositories)**
  - Communicates with MS SQL using Entity Framework Core
  - Implements Repository Pattern for database interactions
- **Additional Components**
  - JWT Authentication for securing API
  - SMTP for email verification & password reset
  - RabbitMQ for event-driven messaging
  - Redis for caching & session management

## 📌 Sections

### **Section 1: Setting Up API with MS SQL Database**
#### UC 1: Configure Database and Application Settings
- Define Database Connection in `appsettings.json`
- Set up Entity Framework Core Migrations
- Implement DbContext for database interaction
- Configure Dependency Injection (DI) for database services

#### UC 2: Implement Address Book API Controller
- Define RESTful Endpoints:
  - `GET /api/addressbook` → Fetch all contacts
  - `GET /api/addressbook/{id}` → Get contact by ID
  - `POST /api/addressbook` → Add a new contact
  - `PUT /api/addressbook/{id}` → Update contact
  - `DELETE /api/addressbook/{id}` → Delete contact
- Use `ActionResult<T>` to return JSON responses
- Test using Postman or CURL

### **Section 2: Implementing DTO and Business Logic**
#### UC 1: Introduce DTO and Model for Address Book
- Create Model Class (`AddressBookEntry`)
- Implement DTO Class to structure API responses
- Use AutoMapper for Model-DTO conversion
- Validate DTOs using FluentValidation

#### UC 2: Implement Address Book Service Layer
- Create `IAddressBookService` interface
- Implement `AddressBookService`:
  - Move logic from controller to service layer
  - Handle CRUD operations in business logic
- Inject Service Layer into Controller using Dependency Injection

### **Section 3: Implementing User Authentication**
#### UC 1: Implement User Registration & Login
- Create `User` Model & DTO
- Implement Password Hashing (BCrypt)
- Generate JWT Token on successful login
- Store User Data in MS SQL Database
- **Endpoints:**
  - `POST /api/auth/register`
  - `POST /api/auth/login`

#### UC 2: Implement Forgot & Reset Password
- Generate Reset Token
- Send Password Reset Email (SMTP)
- Verify token & allow password reset
- **Endpoints:**
  - `POST /api/auth/forgot-password`
  - `POST /api/auth/reset-password`

### **Section 4: Implementing Advanced Features**
#### UC 1: Integrate Redis for Caching
- Store Session Data in Redis
- Cache Address Book Data for faster access
- Improve performance & reduce DB calls

#### UC 2: Integrate RabbitMQ for Event-Driven Messaging
- Publish events when:
  - New user registers (Send email)
  - Contact is added to Address Book
- Consume messages asynchronously

### **Section 5: API Testing & Documentation**
#### UC 1: Document API with Swagger
- Enable Swagger UI for API testing
- Define request/response models in Swagger
- Auto-generate API documentation

#### UC 2: Test API using CURL Commands
- Test User Authentication
- Test CRUD operations for Address Book
- Validate Email Sending, JWT, and Redis

## 📌 API Endpoints

### 🔹 User Authentication API
| Method | Endpoint | Description |
|--------|---------|-------------|
| POST   | /api/auth/register | Register a new user |
| POST   | /api/auth/login | User login with JWT authentication |
| POST   | /api/auth/forgot-password | Send password reset email |
| POST   | /api/auth/reset-password | Reset password using token |
| GET    | /api/auth/profile | Get logged-in user profile |

### 🔹 Address Book API
| Method | Endpoint | Description |
|--------|---------|-------------|
| GET    | /api/addressbook | Get all contacts |
| GET    | /api/addressbook/{id} | Get contact by ID |
| POST   | /api/addressbook | Add new contact |
| PUT    | /api/addressbook/{id} | Update contact |
| DELETE | /api/addressbook/{id} | Delete contact |

## 📌 Relationship Between Address Book and User Authentication

### 🔹 How User Authentication Relates to Address Book?
- Each user should have their own address book entries.
- Only authenticated users can access their own contacts.
- Users should not be able to see or modify other users' address book entries.
- User roles can determine if they can manage other users’ contacts (e.g., Admin).
- JWT authentication ensures secure access to contacts.
- Caching with Redis speeds up access to frequently used contacts.

### 🔹 Summary
| Use Case | Feature | Who Can Access? |
|----------|---------|-----------------|
| User Registration | Sign up new users | Public |
| User Login | Authenticate & get JWT | Public |
| Forgot Password | Reset password via email | Public |
| Create Contact | Add a new contact | Authenticated User |
| Get Contacts | Retrieve all user-specific contacts | Authenticated User |
| Get Contact by ID | Fetch a specific contact | Authenticated User |
| Update Contact | Modify a specific contact | Authenticated User |
| Delete Contact | Remove a contact | Authenticated User |
| Admin View All Contacts | View all user contacts | Admin |
| Admin Delete Contact | Remove any user’s contact | Admin |

---


