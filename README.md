# Library\_Database

## Configuration 

### Database
To Configure the database properly, two scripts need to be run. Both scripts are 
located in the Library\_Database/LibraryDatabaseWPF/Scripts directory

- Full\_Data\_Script.sql
- Procedures\_Script.sql

The best way would be to open each file, copy the contents of the file, and paste
them into a new query against the database

#### Issues
Sometimes the Full\_Data\_Script.sql script will throw errors about not being able 
to DROP tables because of FOREIGN KEYS. That is resolved by running the script 
again or until it does not throw errors about being able to DROP the tables.

You should be able to ignore all other errors from the script. 

If the Procedures\_Script.sql throws errors about not being able to DROP PROCEDURES, 
re-run the script until you do not see those errors. 

### Connection
The connection string variables need to be updated in two places. 

- Library\_Database/LibraryDatabaseWPF/MainWindow.xaml.cs file
- Library\_Database/LibraryDatabaseWPF/ViewModle.cs

The string should be in the format of

```C#
private readonly string connectionString = @"Data Source = <DB FQDN OR DB IP>; Database = <DATABASE NAME>; User ID = <DB USERNAME>; Password= <DB PASSWORD>";
```

## Functionality

### Working
The functionality that is currently implemented

- Ability to see the entire list of books
- Ability to see the entire list of users
- Ability to generate user reports
- Ability to look up books by name, isbn and author
- Ability to look up users by name and user ID
- Ability to checkout books to certain users
- Ability to list top 10 users by number of checkouts
- Incrementing of total checkouts whenever a user checkouts

