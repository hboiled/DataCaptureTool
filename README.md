# We Spy On You Enterprises -- Self-Service Data Capture Tool

### Live Deployment to Azure:
https://wespyonyoudatacapture.azurewebsites.net/

### Build Status:
[![Build status](https://dev.azure.com/sfleeRMS/WeSpyDataCapture/_apis/build/status/WeSpyDataCapture-ASP.NET-CI)](https://dev.azure.com/sfleeRMS/WeSpyDataCapture/_build/latest?definitionId=3)

## Build Instructions:
Beginning from the GitHub repository:
- Ensure dotnet Core SDK is installed (application built using version 3.1.x)
- Clone project into suitable location
- Open in Visual Studio and build solution
- Make sure to enable development SSL certificate for HTTPS
- All dependencies should be downloaded from the build command
- Ctrl + F5 to run in development without debugging
- Tests can be run using the Visual Studio test runner

## Operating Instructions:
- Upon loading the home page, do as the page instructs and click the hyperlink "here" to load the form 
- Alternatively click "Add an Entry" as displayed in the navbar
- Enter user information, the form will display validation errors so that the user knows how to properly format their information
- Click submit and the user will be redirected to a detailed view of their information, summarised
- Click "Back to List" or "View Entries" from the navbar to see all users who have submitted their data

## Dependencies:
- Sqlite Data, Dapper and Moq
- Sqlite DB is bundled with the project and works as is
- The test project is based on the standard Visual Studio XUnit template

## Design Choices

- DataAccess folder coupled with RazorPages project:
I have decided to keep everything in the one project for the sake of simplicity. However, I've structured the folders so that if the app needs to be extended, it is simple enough to extract the DataAccess code into its own project. The intention is for the DataAccess folder to represent a pure data access library.

- Customer Details view model:
The address, phone number, drivers license properties have been made into their own class. While the requirements currently indicate the properties are simple values, and not complex objects, this design choice allows for them to be further extended even though they currently have no real usage. For example, if we want phone numbers to have a boolean property that indicates whether it is a mobile number or if it is the primary contact number, we can simply add such a property to the model and to the phone numbers table. If we had stored the phone number as a simple value in the customer details table, we'd have to remove all phone numbers,create a new table for phone numbers and rewrite more of our application than we would otherwise have to.

- Transactions:
I implemented the SaveCustomerDetails method to just make separate calls to save data, instead of being done so in a transaction. Transactions offer a data management improvement to the application, as it will protect the database from incomplete or invalid data populating its tables. This would result in a more resilient application. I have implemented a transaction before in a demo application, but I am not 100% comfortable with its reliability, so I did not implement it in this case. For production-ready code, I think it is better to not implement features the developer isn't completely comfortable with.

## Screenshots:
![Home](https://github.com/hboiled/DataCaptureTool/blob/main/screenshots/home.png?raw=true)

![Details View](https://github.com/hboiled/DataCaptureTool/blob/main/screenshots/detailsview.png?raw=true)

![Form Validation](https://github.com/hboiled/DataCaptureTool/blob/main/screenshots/formvalidation.png?raw=true)

![List View](https://github.com/hboiled/DataCaptureTool/blob/main/screenshots/listview.png?raw=true)
