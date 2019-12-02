
![alt text](https://github.com/Andreza-Dias-Lima/Projeto-PostTwitter/blob/master/twitter-logo_.png)

# Projeto-PostTwitter
---------------------
   
 * Introdução
 * Requirimentos
 * Instalação
 * Configuração

INTRODUÇÃO
------------

Projeto criado para coletar as últimas postagens do Twitter, dada uma determinada #tag.

 * Para a descrição da api do twitter, visite essa pagina:
   https://developer.twitter.com/en/docs/tweets/search/api-reference/get-search-tweets

REQUIRIMENTOS
------------

Para acessar esse projeto precisará ter acesso a essa ferramentas

 * VS 2019 - API e Web
 * SQLSERVER - Banco de Dados
 * VSCODE - Página em Angular

INSTALAÇÃO
------------
 
 * Install
   https://visualstudio.microsoft.com/vs/
   https://code.visualstudio.com/
   
   
INSTRUÇÕES
-------------

 * SQLSERVER - Rodar o script de criação do banco de dados - Criação dos Campos.sql;
 
 * Web - Para buscar as tags e persistir no banco abrir no VS2019 o projeto "PostTwitter.sln" setar como principal "PostTwitter.web";

 * API - Para acessar a api no VS2019 o projeto "PostTwitter.sln" setar como principal "PostTwitter.API", segue os endereços dos métodos:
 
 get - getAll() = retorna todos os posts coletados durante a busca do projeto web;
           http://localhost:63328/api/posts/getAll;
           
 get - getUsuarioMaxSeguidores() = retorna os 5 (cinco) usuários, da amostra coletada, que possuem mais seguidores;
   http://localhost:63328/api/posts/getAll;
 
 get - getTotalPostPorData() = retorna o total de postagens, agrupadas por hora do dia;
   http://localhost:63328/api/posts/getAll;
  
 get - getTotalTagsPorLocal() = retorna o total de postagens para cada uma das #tag por idioma/país do usuário que postou;
   http://localhost:63328/api/posts/getAll;
 
 post - add(post) = inclui os posts;
   http://localhost:63328/api/posts/getAll;
   
    
LOGGING
-------------

Para acessar o arquivo de logs acessar a pasta PostTwitter.api\Logs
arquivo exemplo mylog-20191201.txt

   
   

