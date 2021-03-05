# NewsPortal Backend Task Application

## Description

This RestFul API is built using .Net Core WebApi with a Sqlite database.

## Instructions

1. Clone the repository from  `https://github.com/eficet/NewsPortal.git`.
2. To install the application dependencies execute the folowing command :

```bash
$ dotnet restore
```

3. To Install the database, you simply have to do nothing when you run the application the first time it will automaticly migrate and Seed the admin and 20 news to the database.
4. To use the unit test just hit the following command and it will provide you with the results of the test
```bash
dotnet test
```
5. To test the Application you can use Postman, In the documentaion you will see a detailed explanation of the routes and what they do.

## Running the app

```bash
dotnet run
```


### Documents

App user Entity:
| Field        | Type                      |
| ------------ | ------------------------- |
| id         | Unique id for the user    |
| Username    | String                    |
| PasswordSalt     | byte[]                    |
| PasswordHash        | byte[]                    |
| UserRole | Enum of user type |

News Entity 
| Field        | Type                      |
| ------------ | ------------------------- |
| id         | Unique id for the News    |
| Title    | String                    |
| NewsType     | Enum                   |
| Text        | String                    |
| CreatedBy        | String                    |
| UpdatedBy        | String                    |
### Routes

#### User Routes:

1. Route URL: `http://localhost:5000/users/login`
   Allows the user to login and returns a token.
    To login as admin:

```
{
  username:"admin",
  password:"admin2021"
}
```


#### News Routes:

1. Route URL: `http://localhost:5000/api/news`
   On this route you can prefrorm GET request which get all the news.
2. Route URL: `http://localhost:5000/api/news/{put your id}`
   On this route you can prefrorm GET request which get a specific news by its Id.
3. Route URL: `http://localhost:5000/api/news/search?query= YourSearch`
   On this route you can prefrorm Get request to search for news with a specific word in the title.

#### News Admin Routes:
1. Route URL: `http://localhost:5000/api/admin/news`
   This route allows admin to prefrorm GET request which get all the news.
2. Route URL: `http://localhost:5000/api/admin/news/{put your id}`
   This route allows admin to prefrorm GET request which get a specific news by its Id.
3. Route URL: `http://localhost:5000/api/admin/news/search`
   This route allows admin to prefrorm POST request to search for news with a specific Search options.Your request body should hold
   the options as the folowing :
   
```
{
  "Title": "News"
  "createdBy": "seeder"
  "updatedBy":"Admin"
}
```

   NOTE: None of the options are mendatory, the option you specify will be checked and applyed. If you send an empty json then you will get all the news.
4. Route URL: `http://localhost:5000/api/admin/news`
   This route allows admin to prefrorm POST request which will add news to the our protal.
5. Route URL: `http://localhost:5000/api/admin/news/{put your id}`
   This route allows admin to prefrorm POST request which will update a specific news by its Id.
#### Sincerely

- Developer - Fayiz Hamad
