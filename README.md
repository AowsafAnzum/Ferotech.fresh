# BookshopThis project is designed to build a Library Management System divided into two main independent parts, allowing both team members to work in parallel with minimal dependencies. Each part covers both entity management and process-oriented modules.

🧩 Project Structure Overview
Part	Area of Focus	Assigned To
Part 1	User & Financial Management	Member 1
Part 2	Book Catalog & Circulation	Member 2
👤 Part 1: User & Financial Management
Assigned to: Member 1

This part focuses on managing all users of the system — both members and staff — along with handling financial aspects like fines and payments.

🧍‍♂️ Member / Student Management (Actor)
Database Schema:

Members table with fields such as MemberId, MemberName, MemberType, ContactInfo, Status, ExpiryDate, etc.
UI/UX:

Form for new member registration
Form to edit existing member details
View to list all members with search and filter options
Backend/Logic:

Functions to register and update members
Logic to manage membership status (Active/Inactive) and track expiry dates
👩‍💼 Staff / Librarian Management (Actor)
Database Schema:

Staff table with fields such as StaffId, FullName, Role, Username, Password, etc.
UI/UX:

Forms to add and manage staff
Login interface for system access
Backend/Logic:

User authentication and role-based access control (Admin, Librarian)
Secure password handling using hashing
💰 Fine & Payment Management (Process)
Database Schema:

Fines table with fields such as FineId, MemberId, IssueId, FineAmount, PaymentStatus, etc.
UI/UX:

View for librarians to see outstanding fines per member
Interface to record fine payments
Backend/Logic:

Handle fine records created from overdue returns
Update and track payment status (Paid/Unpaid)
📖 Part 2: Book Catalog & Circulation
Assigned to: Member 2

This part focuses on managing the library’s inventory (books) and handling core operations like issuing and returning books.

📚 Book Catalog / Master Data Management (Actor)
Database Schema:

Books table with fields such as BookId, Title, Author, Category, ISBN, Status, etc.
UI/UX:

Form to add new books
Form to edit existing book details
View to list books with search and filter functionality
Backend/Logic:

Add, update, and delete book records
Implement search and filter logic
Manage book status: Available, Issued, Damaged, Lost
🔄 Book Issue Management (Process)
Database Schema:

BookIssue table with fields such as IssueId, MemberId, BookId, IssueDate, DueDate, etc.
UI/UX:

User-friendly interface to issue a book
Search for available books and active members
Backend/Logic:

Record issue transactions
Automatically update book status to "Issued"
Auto-calculate and store due dates
Enforce borrowing limits per member
📥 Book Return Management (Process)
Database Schema:

Logic to handle returns, e.g., by updating the BookIssue table or maintaining a BookReturn log
UI/UX:

Interface to process book returns via issue record lookup
Backend/Logic:

Record returns and update book status to "Available"
Check if overdue and automatically create Fine records in the Fines table
🤝 Shared Responsibilities
Responsibility	Description
Initial Database Design	Both members collaborate to define the database schema and relationships before starting development.
Reporting & Analytics	Split by module: Member 1 handles user/financial reports, Member 2 handles book/circulation reports.
Notifications & Alerts	Shared responsibility — divided based on module needs.
Testing & Deployment	Each member tests their modules independently, followed by integration and system-level testing.
🏗️ Development Phases
Phase 1: Database design & schema alignment
Phase 2: Independent module development
Phase 3: Integration & end-to-end testing
Phase 4: UI/UX refinement and deployment
⚙️ Suggested Tech Stack
Layer	Technology
Frontend	HTML, CSS, JavaScript, Bootstrap
Backend	PHP / ASP.NET / Node.js
Database	MySQL / SQL Server
Version Control	Git & GitHub
🧠 Collaboration Guidelines
Commit frequently with descriptive messages
Use feature branches for each module
Review and merge via pull requests
Keep shared documentation (API, schema, workflows) updated
