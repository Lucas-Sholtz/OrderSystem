SELECT AVG(Product.ProductPrice)
FROM Product
WHERE Product.ShopID = (SELECT ShopID
						FROM Shop
						WHERE Shop.ShopID IN 
						(SELECT Courier.ShopID 
						FROM Courier 
						WHERE CourierName = PLACEHOLDER1));