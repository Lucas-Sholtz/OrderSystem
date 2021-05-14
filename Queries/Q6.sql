SELECT DISTINCT Product.ProductName
FROM Shop INNER JOIN Product ON Product.ShopID = Shop.ShopID
WHERE ShopName = PLACEHOLDER1 INTERSECT (SELECT DISTINCT Product.ProductName
FROM Shop INNER JOIN Product ON Product.ShopID = Shop.ShopID
WHERE ShopName = PLACEHOLDER2)