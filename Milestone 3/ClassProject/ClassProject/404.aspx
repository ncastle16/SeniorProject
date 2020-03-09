﻿<% Response.StatusCode = 404 %>

<!DOCTYPE html>
<html lang="en">

<head>
  <style type="text/css">
    html,
    body {
      margin: 0;
      padding: 0;
      height: 100%;
    }

    body {
      vertical-align: middle;
      font-family: 'Lato', Sans-Serif;
      background-color: #2D72D9;
      color: #fff;
      -moz-font-smoothing: antialiased;
      -webkit-font-smoothing: antialiased;
    }

    .error-container {
      text-align: center;
    }

    }

    .error-container h1 {
      margin: 100px;
      
      font-size: 330px;
      font-weight: 300;
    }

    .return {
      color: rgba(255, 255, 255, 0.6);
      font-weight: 400;
      letter-spacing: -0.04em;
      margin: 0;
    }


    .return a {
      padding-bottom: 1px;
      color: #fff;
      text-decoration: none;
      border-bottom: 1px solid rgba(255, 255, 255, 0.6);
      -webkit-transition: border-color 0.1s ease-in;
      transition: border-color 0.1s ease-in;
    }

    .return a:hover {
      border-bottom-color: #fff;
    }
  </style>
</head>
<div class="error-container">
<img src="https://i.imgur.com/fRwHBlp.png" style="width: 50%; height: 50%;"/>
  <h1>Uh oh- you look like you need rescued.</h1>
  <p class="return">Let's get you out of <a href="/">uncharted waters</a></p>
</div>

</body>
</html>
