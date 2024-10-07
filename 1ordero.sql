use apteka;
CREATE TABLE ordero
(
    id integer auto_increment primary key,
    status varchar(50),
    delivdate datetime,
    FOREIGN KEY (id) REFERENCES user (id)
    
   
);