create table customers(
                          customerid serial primary key,
                          firstname varchar(50),
                          lastname varchar(50),
                          city varchar(20),
                          phonenumber varchar(13),
                          pancardno varchar(16),
                          dob date,
                          createdat date,
                          deletedat date
)

create table branches(
                         branchid serial primary key,
                         branchname varchar(50),
                         branchlocation varchar(50),
                         createdat date,
                         deletedat date
)

create table loans(
                      loanid serial primary key,
                      loanamount decimal(10,2),
                      dateissued date,
                      createdat date,
                      deletedat date,
                      customerid int references customers(customerid),
                      branchid int references branches(branchid)
)
create type accountstatus as enum('active', 'inactive')
create type accounttype as enum('premium', 'gold', 'silver')

create table accounts(
                         accountid serial primary key,
                         balance decimal(10,2),
                         accountstatus accountstatus,
                         accounttype accounttype,
                         currency varchar(10),
                         customerid int references customers(customerid),
                         createdat date,
                         deletedat date
)

create type transactionstatus as enum('failed', 'successfull', 'canceled', 'waited')


create table transactions(
                             transactionid serial primary key,
                             transactionstatus transactionstatus,
                             dateissued date,
                             amount decimal(10,2),
                             createdat date,
                             deletedat date,
                             fromaccountid int references accounts(accountid),
                             toaccountid int references accounts(accountid)
)

