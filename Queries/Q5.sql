SELECT SUM(ProductOrderPair.ProductQuantity * Product.ProductPrice) AS M
FROM Product INNER JOIN ProductOrderPair ON Product.ProductID = ProductOrderPair.ProductID
WHERE OrderID = PLACEHOLDER1