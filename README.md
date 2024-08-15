A Aplicação consiste em 2 serviços:

- CashFlow.Financial.API => Pode inserir operações de débito e de crédito, com banco de dados SQLServer
- CashFlow.Reports.API => Disponibiliza o consolidado do dia das operações de crédito e débito efetuadas pelo serviço Financeiro, com banco de dados MongoDB

Para subir a aplicação executar o comando abaixo no diretório "src":

- docker-compose up -d

Para desfazer o ambiente executar o comando abaixo no diretório "src":

- docker-compose down

Para testes de funcionalidade foi disponibilizado um arquivo do Postman na pasta "\Postman Collection";
