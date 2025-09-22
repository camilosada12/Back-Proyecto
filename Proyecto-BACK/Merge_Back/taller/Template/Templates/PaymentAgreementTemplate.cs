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
      color: #000;
      margin: 40px;
    }
    h1 {
      text-align: center;
      color: #2c3e50;
      margin-bottom: 10px;
    }
    h2 {
      margin-top: 25px;
      color: #2c3e50;
    }
    .section {
      margin-top: 15px;
    }
    .info p {
      margin: 3px 0;
    }
    table {
      width: 100%;
      border-collapse: collapse;
      margin-top: 15px;
    }
    table, th, td {
      border: 1px solid #333;
    }
    th {
      background-color: #f2f2f2;
    }
    th, td {
      padding: 6px;
      text-align: center;
    }
    .footer {
      margin-top: 40px;
      text-align: center;
      font-style: italic;
      font-size: 10pt;
      color: #555;
    }
  </style>
</head>
<body>
  <h1>📄 Acuerdo de Pago </h1>
  <p style=""text-align:center; font-size:10pt; color:#666;"">
    Generado por el Sistema de Gestión de Multas
  </p>

  <div class=""section"">
    <p><strong>Nombre:</strong> @Nombre</p>
    <p><strong>Documento:</strong> @Documento (@TipoDocumento)</p>
    <p><strong>Dirección:</strong> @Direccion</p>
    <p><strong>Barrio:</strong> @Barrio</p>
    <p><strong>Teléfono:</strong> @Telefono</p>
    <p><strong>Correo:</strong> @Correo</p>
  </div>

  <div class=""section"">
    <p><strong>Inicio del acuerdo:</strong> @FechaInicio</p>
    <p><strong>Fin del acuerdo:</strong> @FechaFin</p>
    <p><strong>Fecha expedición cédula:</strong> @ExpedicionCedula</p>
    <p><strong>Método de pago:</strong> @MetodoPago</p>
    <p><strong>Frecuencia de pago:</strong> @FrecuenciaPago</p>
    <p><strong>Cuotas pactadas:</strong> @Cuotas</p>
  </div>

  <div class=""section"">
    <p><strong>Tipo de infracción:</strong> @TipoInfraccion</p>
    <p><strong>Detalle de infracción:</strong> @Infraccion</p>
    <p><strong>Valor SMDLV:</strong> @ValorSMDLV</p>
  </div>

  <h2>💰 Información financiera</h2>
  <table>
    <thead>
      <tr>
        <th>Monto Base</th>
        <th>Intereses</th>
        <th>Saldo Pendiente</th>
        <th>Cuotas</th>
        <th>Valor Cuota</th>
      </tr>
    </thead>
    <tbody>
      <tr>
        <td>@MontoBase</td>
        <td>@Intereses</td>
        <td>@SaldoPendiente</td>
        <td>@Cuotas</td>
        <td>@ValorCuota</td>
      </tr>
    </tbody>
  </table>

  <div class=""section"">
    <p><strong>Estado:</strong> @Estado</p>
    @Coactivo
    @UltimoInteres
  </div>

  <div class=""footer"">
    <p>Este acuerdo de pago se rige por la normatividad vigente en Colombia 
    (Ley 769 de 2002 y demás normas concordantes).</p>
    <p><em>Sistema de Gestión de Multas</em></p>
  </div>
</body>
</html>";

    }
}

