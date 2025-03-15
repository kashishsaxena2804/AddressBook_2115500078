<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Address Book API Documentation</title>
    <style>
        body { font-family: Arial, sans-serif; line-height: 1.6; margin: 20px; }
        h1, h2, h3 { color: #333; }
        table { width: 100%; border-collapse: collapse; margin: 20px 0; }
        table, th, td { border: 1px solid #ddd; }
        th, td { padding: 10px; text-align: left; }
        th { background: #f4f4f4; }
        pre { background: #f4f4f4; padding: 10px; border-radius: 5px; }
    </style>
</head>
<body>

<h1>Address Book ASP.NET Core API</h1>

<h2>📌 Architecture Overview</h2>

<h3>Presentation Layer (API)</h3>
<ul>
    <li>Controllers for AddressBook and User Authentication</li>
    <li>Uses Swagger for API documentation</li>
</ul>

<h3>Business Logic Layer (Services)</h3>
<ul>
    <li>Manages business logic for AddressBook and User Authentication</li>
    <li>Handles password hashing, JWT generation, email sending</li>
</ul>

<h3>Data Access Layer (Repositories)</h3>
<ul>
    <li>Communicates with MS SQL using Entity Framework Core</li>
    <li>Implements Repository Pattern for database operations</li>
</ul>

<h3>Security & Communication</h3>
<ul>
    <li>JWT Authentication</li>
    <li>SMTP for email verification & password reset</li>
    <li>RabbitMQ for event-driven communication</li>
    <li>Redis for caching and session management</li>
    <li>Hashing (Salting) for password encryption</li>
</ul>

<h2>📂 API Endpoints</h2>

<h3>🔹 User Authentication API</h3>
<table>
    <tr>
        <th>Method</th>
        <th>Endpoint</th>
        <th>Description</th>
    </tr>
    <tr>
        <td>POST</td>
        <td>/api/auth/register</td>
        <td>Register a new user</td>
    </tr>
    <tr>
        <td>POST</td>
        <td>/api/auth/login</td>
        <td>User login with JWT authentication</td>
    </tr>
    <tr>
        <td>POST</td>
        <td>/api/auth/forgot-password</td>
        <td>Send password reset email</td>
    </tr>
</table>

<h3>🔹 Address Book API</h3>
<table>
    <tr>
        <th>Method</th>
        <th>Endpoint</th>
        <th>Description</th>
    </tr>
    <tr>
        <td>GET</td>
        <td>/api/addressbook</td>
        <td>Get all contacts</td>
    </tr>
    <tr>
        <td>GET</td>
        <td>/api/addressbook/{id}</td>
        <td>Get contact by ID</td>
    </tr>
</table>

<h2>📌 Relationship Between Address Book and User Authentication</h2>
<ul>
    <li>Each user should have their own address book entries.</li>
    <li>Only authenticated users can access their own contacts.</li>
    <li>Users should not be able to see or modify other users' address book entries.</li>
</ul>

</body>
</html>
