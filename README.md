## CanopyViewer
A full-stack computerized maintenance management system built with C#/ASP.NET, Razor pages and PostgreSQL.

## About
My senior project for the Software Engineering program at Oregon Institute of Technology. It is inspired by my previous work experience in operations, facilities and maintenance and the difficulties I experienced with other CMMSs, namely speed and unintuitive interfaces. The application has full user account management, user roles, asset management, and work order creation and tracking including automatic work order creation for routine preventative maintenance.

## Tech Stack
- ASP.NET Core w/ Razor Pages (8.0.11)
- C#
- PostgreSQL with Entity Framework Core (Npgsql 8.0.11)
- HTML, CSS, JavaScript

## Features
- Work order creation, assignment, and status tracking
- Asset management with maintenance history
- Work order creation with recurrence schedules
- Work order and asset linking
- User authentication, management, and roles with access control

## Prerequisites
- .NET 8 SDK

## Setup

1. Clone repo
2. From project root, run 'dotnet run'
3. Navigate to 'https://localhost:5000' by default

## Structure
- `Models/` — Entity classes and data models
- `Pages/` — Razor Pages (views and page models)
- `Services/` — Business logic layer
- `Data/` — Database context and configuration
- `Migrations/` — Entity Framework migration history
- `wwwroot/` — Static assets (CSS, JavaScript)

## Work-In-Progress
- Email notifications
- Reporting and export functionality
- Bulk asset import
- Change database to PostgreSQL ✅ 