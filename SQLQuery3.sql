﻿select Categories.Name, users.UserName from Categories join AspNetUsers users on Categories.Owner_Id = users.Id