# CodingLife()
E-Commerce site for every type of products.

![image](https://user-images.githubusercontent.com/32294561/114141047-c13a7d00-9919-11eb-8391-b7b1d722585b.png)
![image](https://user-images.githubusercontent.com/32294561/114141189-edee9480-9919-11eb-9253-c3c12e95c988.png)
![image](https://user-images.githubusercontent.com/32294561/114141335-21312380-991a-11eb-9ae8-6aa76af5127d.png)

# Integrations
- Admin can create Products that have Categories, Colors, Sizes
- Add products to Shopping Cart
- Add products to Wish List
- User can order different products
- Admin can create different promo codes with % discount
- User can apply promo code for discount to his order
- User can create account and watch his orders, orders statuses and promo codes assigned

# External Integrations
- Econt ["Get all offices in Bulgaria and list them", "Validate user address when typed in" ]
- Cloudinary ["For storing all product images there"]
- SendGrid ["Email sender"]
- HighCharts ["Charts for different statistics, for ex. 'Total orders in last 7 days'"]

# INITIAL SETUP
To setup the project you need to run additionally this command for Distributed Cache Table in Database:
## dotnet sql-cache create "Server=.\SQLEXPRESS;Database=CodingLife;Trusted_Connection=True;MultipleActiveResultSets=true" dbo Cache
