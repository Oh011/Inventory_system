# -Inventory_System
 Inventory &amp; Order Management System built with ASP.NET Core &amp; Clean Architecture. Features include role-based authentication, product &amp; category management, purchase orders, sales invoicing, real-time stock control, PDF export, notifications, and reporting.

 Designed for **scalability**, **testability**, and **real-world business workflows** such as product tracking, sales invoicing, stock adjustment, and purchase order lifecycle handling — including **Domain Events**, **Background Services**, and **Real-Time Notifications using SignalR**.

📄 **API Documentation:** [View PDF](docs/API_Documentation_Inventory System.pdf)


 ## 🏗️ Architecture Highlights
- ✅ **Clean Architecture** (Domain, Application, Infrastructure, WebAPI)
- ✅ **CQRS** with MediatR for commands and queries
- ✅ **Domain Events** for triggering side effects like:
  - 📧 Sending emails when a Purchase Order is Created, Received, or Cancelled
  - 🔔 Sending real-time SignalR notifications
  - 📉 Automatically checking for low-stock products after stock adjustments
- ✅ **SignalR** for real-time notifications (e.g., low stock alerts, PO updates)
- ✅ **Background Services** for deferred or long-running domain operations
- ✅ **Role-based Authorization** (Admin, Manager, Sales, Warehouse)
- ✅ **Soft Deletion**, Optimistic Concurrency, FluentValidation pipeline
- ✅ Applied the Publish/Subscribe pattern using Domain Events, SignalR, and Background Services to decouple workflows (e.g., triggering real-time notifications and emails when purchase orders are created or updated).



## 🔥 Key Features

- 🔐 **Authentication & Refresh Tokens**
- 🧾 **Sales Invoicing**
- 📦 **Inventory Control** with value reports & manual adjustments
- 🛒 **Purchase Orders** (create, cancel, receive, export to PDF)
- 🧍 **Employee, Customer & Supplier Management**
- 📊 **Sales Reports** + PDF export
- 🔔 **Real-time Notifications with SignalR**
- 📃 **Stock Adjustment Logs** + PDF export

## 📬 Notable Domain Events

- 🟡 `PurchaseOrderStatusChangedDomainEvent`:  
  - Sends SignalR notifications to warehouse users  
  - Emails supplier with the order details

- 🔴 `ProductStockDecreasedDomainEvent`:  
  - Automatically triggers low stock warning  
  - Sends SignalR notification to managers

---

## 🧠 How CQRS Works Here

- Commands → Handlers → Services for business logic
- Queries → Handlers for read-only views
- MediatR decouples the flow
- Validators run in the pipeline before command handlers

---

## 📡 Real-Time with SignalR

- `NotificationHub` broadcasts:
  - 🔔 New domain event-based notifications
  - 📉 Low-stock warnings
  - ✅ Purchase order status updates
