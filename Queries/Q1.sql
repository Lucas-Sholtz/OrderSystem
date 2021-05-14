SELECT ShopName
FROM Shop
	WHERE Shop.ShopID IN 
	(SELECT Courier.ShopID 
	FROM Courier 
	WHERE CourierName = PLACEHOLDER1);