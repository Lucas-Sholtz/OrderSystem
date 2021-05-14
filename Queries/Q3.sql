SELECT Shop.ShopName
FROM Town INNER JOIN (
Street INNER JOIN (
Adress INNER JOIN Shop 
ON Shop.AddressID = Adress.AddressID) 
ON Street.StreetID = Adress.StreetID) 
ON Town.TownID = Street.TownID
WHERE TownName = PLACEHOLDER1