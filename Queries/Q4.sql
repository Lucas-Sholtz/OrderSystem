SELECT Shop.ShopName
FROM Street INNER JOIN (Adress INNER JOIN (Shop INNER JOIN Product ON Shop.ShopID = Product.ShopID) ON Shop.AddressID = Adress.AddressID) ON Street.StreetID = Adress.StreetID
WHERE Product.ProductPrice > PLACEHOLDER1