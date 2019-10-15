SELECT P.PersonId, P.LastName, P.FirstName, P.DateOfBirth, E.Manager_PersonId, E.Pseudo
From [BankDB].[BCR].[Person] P 
INNER JOIN [BankDB].[BCR].[Employes] E
ON E.PersonId = P.PersonId	