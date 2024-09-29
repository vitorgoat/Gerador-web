# Gerador Web

## Descrição

O **Gerador Web** é um projeto desenvolvido com **Asp Net Core MVC** que permite aos usuários:

- **Cadastro e Login**: Usuários podem criar uma conta e fazer login no sistema.
- **Geração de Dados Fictícios**: O sistema gera dados fictícios e os exporta para uma planilha Excel.
- **Envio de E-mail com Anexo**: É possível enviar a planilha gerada como anexo em um e-mail.

O projeto utiliza boas práticas de desenvolvimento e organização, garantindo um código limpo e uma estrutura sólida.

## Funcionalidades

- **Cadastro de Usuários**: Permite que novos usuários se registrem no sistema.
- **Login de Usuários**: Usuários registrados podem fazer login para acessar as funcionalidades do sistema.
- **Geração de Planilha Excel**: Gera uma planilha Excel com dados fictícios de clientes.
- **Envio de E-mail**: Envia a planilha gerada como anexo por e-mail.

## Tecnologias Utilizadas

- **ASP Net Core MVC**: Framework para desenvolvimento web.
- **Entity Framework Core**: ORM para manipulação de banco de dados.
- **EPPlus**: Biblioteca para manipulação de arquivos Excel.
- **SMTP**: Protocolo para envio de e-mails.

## Instruções para Execução

Caso queira rodar o projeto em sua máquina local, siga os passos abaixo:

1. **Clone o Repositório**
    
    Clone o repositório para sua máquina local usando o comando:
    
    ```bash
    git clone <https://github.com/vitorgoat/gerador-web.git>
    
    ```
    
2. Abra-o em sua IDE, e coloque como inicializacao o [Atak.Web](http://atak.web/), e entao build com Http.
3. Crie um cadastro, respeitando as instrucoes de senhas
4. Teste a geracao de arquivos, e o disparo de e-mail.

## Instruções para consultas de tabelas no banco

1. baixe o SQLITE (https://sqlitebrowser.org/dl/)
2. Abra o banco na raiz do projeto ([Atak.Web/app.db](http://atak.web/app.db))
3. Execute a consulta `sql` para visualizar os dados estruturais em AspNetusers

## Para rodar a API em sua IDE
1. Em sua IDE defina Atak.API como projeto de inicializacao.
