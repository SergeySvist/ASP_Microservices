using UsersMS.Client;

IUserMsClient client = new UsersMsClient();

var userById = await client.GetUserByID(1);

Console.WriteLine(userById.Name);