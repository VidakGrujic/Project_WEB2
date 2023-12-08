# Project WEB2

This is a project from the course Programming in the Smart GRID systems, also known as WEB2.
Application represents the online web shop where seller can add their products and sell them and customers can buy those products. 

**Programing languages: C# and React.js(Javascript)**

## Description

In this project, there are 3 types of the users:
1. Adminstrator
2. Seller
3. Customer

In order to use the functionalities of the application, users have to register, if they do not have a profile, or to login if they have. There are 2 possible way to register
1. Ordinary registration - input fields are filled with the user's information: email, password, username, name and lastname, date of birth, adress, type of the user (seller or customer) and profile image. Note: Administrators are added manually.
2. Registration via gmail.

For the sellers, administrators have to verify their account.

After successful registration or log in, users can use the functionalities of the application and they are:
1. Edit profile (all type of users can use it)
2. Add product to sell (only seller)
3. Make a new order (only customer)
4. See previous orders (only customer)
5. See new orders which need to be sent (only seller)
6. See old orders which were sent (only seller)
7. Verification of sellers (only administrator)
8. See all orders, new and old (only administrato)

### 1. Edit profile
Users can change their profile data

### 2. Add product to sell
Seller can add their product which want to sell. Information about products are: name, price, available amount, description and product image.

### 3. Make a new order
Customer have a list of products and clicking on the button "Order", the new page is opened and amount of the product is chosen. Then the full price of order is calculated and shown on the screen. 

### 4. See previous orders
Customer can see their previous orders, where list of ordered products are displayed.

### 5. See new orders which need to be sent
Seller can see information about the products that customer has ordered. If customer has ordered products from different seller, the order will be displayed to every seller, but they will see only their product.

### 6. See old orders which were sent
Seller can se old orders which include their products

### 7. Verification of sellers
Administrator can accept or deny the new seller registration. In both cases, the seller will be announced about their registration status via gmail. (If their registration is accepted or denied)

### 8. See all orders, new and old
Administrator is able to see every order, for every customer.

## Implementation details

For database is used .NET Entity Framework, Model to Data approach. For backend is used .NET CORE. For fronted is used React.js library, written in Javascript. REST convention is fulfilled. Password are hashed in the database.
Authentication and authorization are implemented using token which user use for the current session. Token is got after registration and login and sent with every HTTP request from frontend to backend.  


  
