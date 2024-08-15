A Aplicação consiste em 2 serviços e é necessario ter o Docker instalado na máquina:

Serviços:

- CashFlow.Financial.API => Pode inserir operações de débito e de crédito, com banco de dados SQLServer
- CashFlow.Reports.API => Disponibiliza o consolidado do dia das operações de crédito e débito efetuadas pelo serviço Financeiro, com banco de dados MongoDB

Para subir a aplicação você deve executar o comando abaixo no diretório "src":

- docker-compose up -d

Para desfazer o ambiente você deve  executar o comando abaixo no diretório "src":

- docker-compose down

Considerando alguns ambientes e infraestruturas melhor executar com o Visual Studio Community 2022.

Para testes de funcionalidade foi disponibilizado um arquivo do Postman na pasta "\Postman Collection".
