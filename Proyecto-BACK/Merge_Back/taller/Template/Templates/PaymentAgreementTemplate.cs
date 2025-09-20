using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Template.Templates
{
    public static class PaymentAgreementTemplate
    {
        public static readonly string Html = @"
<!DOCTYPE html>
<html lang=""es"">
<head>
  <meta charset=""UTF-8"">
  <style>
    body {
      font-family: Arial, sans-serif;
      font-size: 12pt;
      line-height: 1.5;
      color: #000;
      margin: 40px;
    }

    h1 {
      text-align: center;
      color: #2c3e50;
      border-bottom: 2px solid #4CAF50;
      padding-bottom: 10px;
      margin-bottom: 20px;
    }

    .section {
      margin-top: 20px;
    }

    .label {
      font-weight: bold;
      color: #555;
    }

    .value {
      margin-left: 5px;
    }

    table {
      width: 100%;
      border-collapse: collapse;
      margin-top: 15px;
    }

    table, th, td {
      border: 1px solid #ccc;
    }

    th, td {
      padding: 8px;
      text-align: left;
    }

    th {
      background-color: #f4f4f4;
    }

    .footer {
      margin-top: 40px;
      text-align: center;
      font-style: italic;
    }
  </style>
</head>
<body>
  <h1>📑 Acuerdo de Pago</h1>

  <div class=""section"">
    <span class=""label"">Dirección:</span> <span class=""value"">@Direccion</span><br/>
    <span class=""label"">Barrio:</span> <span class=""value"">@Barrio</span><br/>
    <span class=""label"">Teléfono:</span> <span class=""value"">@Telefono</span><br/>
    <span class=""label"">Correo:</span> <span class=""value"">@Correo</span><br/>
  </div>

  <div class=""section"">
    <span class=""label"">Inicio del acuerdo:</span> <span class=""value"">@FechaInicio</span><br/>
    <span class=""label"">Fin del acuerdo:</span> <span class=""value"">@FechaFin</span><br/>
    <span class=""label"">Descripción:</span> <span class=""value"">@Descripcion</span>
  </div>

  <h2>💰 Información financiera</h2>
  <table>
    <tr>
      <th>Monto Base</th>
      <th>Intereses</th>
      <th>Saldo Pendiente</th>
      <th>Cuotas</th>
      <th>Valor Cuota</th>
    </tr>
    <tr>
      <td>@MontoBase</td>
      <td>@Intereses</td>
      <td>@SaldoPendiente</td>
      <td>@Cuotas</td>
      <td>@ValorCuota</td>
    </tr>
  </table>

  <div class=""section"">
    <p><span class=""label"">Estado:</span> @Estado</p>
    @Coactivo
  </div>

  <div class=""footer"">
    <p>Sistema de Gestión de Multas</p>
  </div>
</body>
</html>";
    }
}

