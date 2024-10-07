use apteka;
CREATE TABLE prodord
(
    id integer auto_increment primary key,
    quantity integer(50),
    FOREIGN KEY (id) REFERENCES product (id),
     FOREIGN KEY (id) REFERENCES ordero (id)
   
);