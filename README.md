# BethanysPieShop
Bethanys Pie shop built with ASP.NET Core MVC\

## Live Demo
* Click on this link [here](https://proshop-site.herokuapp.com/)

## Prerequisites
* ...

## Key Features
* ...
  
## Installation
* Download the project
* Create a .env file in the root project folder and add the following
```
    NODE_ENV = development
    PORT = 5000

    MONGO_URI = Your Mongodb URI
    JWT_SECRET = typeinyourownsecret

    PAYPAL_CLIENT_ID = Your PayPal Client ID
```
* Install Dependencies (inside frontend & backend folders)
```
    npm install
    cd frontend
    npm install
```
* Run the app (from root)
```
    # Runs frontend (:3000) & backend (:5000)
    npm run dev
    
    # Run backend only
    npm run server
```
## Build & Deploy
```
    # Create frontend prod build
    cd frontend
    npm run build
```
There is a Heroku postbuild script, so if you push to Heroku, no need to build manually for deployment to Heroku
## Seed Database
...

## Technologies Used
* **Front End**: Javascript, Jquery, Bootstrap
* **Back End**: ASP.NET Core, Entity Framework

## Author
* Rchi Lugtu
## Future Scope
* ...
