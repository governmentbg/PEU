<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" 
                xmlns:r="http://ereg.egov.bg/segment/R-3122" 
                xmlns:duri="http://ereg.egov.bg/segment/0009-000044"
				        xmlns:rsd="http://ereg.egov.bg/segment/0009-000001"
                xmlns:ESPBD="http://ereg.egov.bg/segment/0009-000002"
                xmlns:E="http://ereg.egov.bg/segment/0009-000013"
                xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
                xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
                xmlns:ms="urn:schemas-microsoft-com:xslt" 
                xsi:type="xsl:transform">
  <xsl:include href="./CommonTemplates.xslt"/>
  <xsl:output omit-xml-declaration="yes" method="html"/>

  <xsl:template match="r:ReceiptAcknowledgedPaymentForMOI">
    <xsl:text disable-output-escaping='yes'>&lt;!DOCTYPE html&gt;</xsl:text>
    <html>
      <head>
        <style>
          body, table
          {
          font-family: Verdana;
          font-size: 18px;
          color: black;
          }
          h1
          {
          font-size: 24pt;
          color: black;
          text-align: center;
          }
          h2
          {
          padding-top:5px;
          text-align:center;
          vertical-align:middle;
          }
          hr
          {
          border: 1px solid #000000;
          }
          .mainTable, .mainCell
          { border: 1px solid #000000;
          padding: 0px 5px 5px 5px;
          vertical-align:top;
          }
          .dataLabel
          {
          font-weight: bold;
          }
          .literal
          {
          font-weight: normal;
          }
          .left
          {
          font-size: 12px;
          padding: 3px 5px 3px 5px;
          vertical-align: top;
          width: 150px;
          }
          .right
          {
          font-size: 12px;
          padding: 3px 5px 3px 5px;
          vertical-align: top;
          font-weight: bold;
          }
          .signTable
          {
          border: 1px solid #C0C0C0;
          padding: 0px 5px 5px 5px;
          vertical-align: top;
          }
          .mini
          {
          font-family: Verdana;
          font-size: 11px;
          }
          .header
          {
          background-color: #F0F0F0;
          font-weight: bold;
          color: #666666;
          }
          .left_mini
          {
          width: 160px;
          }
          .textbox
          { border: thin outset #C0C0C0;
          background-color: #FFFFFF;
          min-height: 22px;
          font-size: 15px;
          }
          .coloredTD
          {
          background-color: #DFDFDF;
          font-size: 12px;
          width: 600px;
          }

          .uppercase{
          text-transform: uppercase;
          }
        </style>
      </head>
      <body>
        <table cellpadding="5" cellspacing="5"  border="1" >
          <tr>
            <td class="coloredTD">
              Дата на плащане
              <div>
                <xsl:call-template name="ShowDateInBoxes">
                  <xsl:with-param name="DateTime" select="r:DocumentReceiptOrSigningDate/."></xsl:with-param>
                </xsl:call-template>
              </div>
            </td>
          </tr>
          <tr>
            <td class="coloredTD">
              <table cellpadding="0" cellspacing="0">
                <tr>
                  <td class="coloredTD">
                    Име на получателя
                    <div class="textbox uppercase" style="width:450px;">
                      <xsl:value-of select="r:ElectronicServiceProviderBasicData/ESPBD:EntityBasicData/E:Name" />
                    </div>
                  </td>
                  <td></td>
                </tr>
                <tr>
                  <td class="coloredTD">
                    IBAN на получателя
                    <div class="textbox" style="width:450px;letter-spacing: 5px">
                      <xsl:value-of select="r:IBAN/."/>
                    </div>
                  </td>
                  <td class="coloredTD" align="right">
                    BIC на получателя
                    <div class="textbox" style="letter-spacing: 5px;" >
                      <xsl:value-of select="r:BIC/."/>
                    </div>
                  </td>
                </tr>
                <tr>
                  <td class="coloredTD">
                    При банка - име на банката на получателя
                    <div class="textbox" style="width:450px;">
                      <xsl:value-of select="r:BankName/."/>
                    </div>
                  </td>
                  <td class="coloredTD" align="right">
                  </td>
                </tr>
              </table>
            </td>
          </tr>
          <tr>
            <td class="coloredTD">
              <table cellpadding="0" cellspacing="0">
                <tr>
                  <td class="coloredTD" align="center" style="width:66%">
                    ПРЕВОДНО НАРЕЖДАНЕ<DIV>за плащане от/към бюджета</DIV>
                  </td>
                  <td class="coloredTD">
                    Валута<div>
                      <span class="textbox" style="letter-spacing: 5px">
                        <xsl:value-of select="r:AmountCurrency/."/>
                      </span>
                    </div>
                  </td>
                  <td class="coloredTD" align="right">
                    Сума<div class="textbox" style="letter-spacing: 5px; width:150px;">
                      <xsl:call-template name="FormatCurrency">
                        <xsl:with-param name="Amount" select="r:Amount/."></xsl:with-param>
                      </xsl:call-template>
                    </div>
                  </td>
                </tr>
                <tr>
                  <td class="coloredTD" colspan="3">
                    Основание за плащане
                    <div class="textbox" style="width:610px;">
                      <xsl:value-of select="r:PaymentReason/."/>
                    </div>
                  </td>
                </tr>
                <tr>
                  <td class="coloredTD" colspan="3">
                    Още пояснения
                    <div class="textbox" style="width:610px;"></div>
                  </td>
                </tr>
                <tr>
                  <td class="coloredTD" colspan="2">
                    Документа, по който се плаща
                    <div class="textbox" style="width:340px;">
                      <xsl:call-template name="ShowAISCaseURI"></xsl:call-template>
                    </div>
                  </td>
                  <td class="coloredTD" align="right">
                    Дата на документа
                    <div style=" float: right;">
                      <xsl:call-template name="ShowDateInBoxes">
                        <xsl:with-param name="DateTime" select="r:AISCaseURI/duri:DocumentURI/rsd:ReceiptOrSigningDate/."></xsl:with-param>
                      </xsl:call-template>
                    </div>
                  </td>
                </tr>
                <tr>
                  <td class="coloredTD" colspan="2">
                    Период, за който се отнася плащането - от дата
                    <div></div>
                  </td>
                  <td class="coloredTD" align="right">
                    до дата
                    <div style=" float: right;"></div>
                  </td>
                </tr>
              </table>
            </td>
          </tr>
          <tr>
            <td class="coloredTD">
              <table cellpadding="0" cellspacing="0">
                <tr>
                  <td class="coloredTD">
                    Задължено лице - име
                    <div class="textbox" style="width:450px;">
                      <xsl:call-template name="ShowElectronicServiceApplicantNames"></xsl:call-template>
                    </div>
                  </td>
                </tr>
                <tr>
                  <td class="coloredTD">
                    EИК/БУЛСТАТ/ЕГН/ЛНЧ
                    <div class="textbox" style="width:150px;">
                      <xsl:call-template name="ShowIdentifier"></xsl:call-template>
                    </div>
                  </td>
                </tr>
              </table>
            </td>
          </tr>
          <tr>
            <td class="coloredTD">
              <table cellpadding="0" cellspacing="0">
                <tr>
                  <td class="coloredTD">
                    Име на наредителя
                    <div class="textbox" style="width:450px;"></div>
                  </td>
                  <td></td>
                </tr>
                <tr>
                  <td class="coloredTD">
                    IBAN на наредителя
                    <div class="textbox" style="width:450px;letter-spacing: 5px;"></div>
                  </td>
                  <td class="coloredTD" align="right">
                    BIC на наредителя
                    <div class="textbox" style="letter-spacing: 5px;" ></div>
                  </td>
                </tr>
                <tr>
                  <td class="coloredTD">
                    При банка - име на банка на наредителя
                    <div class="textbox" style="width:450px;"></div>
                  </td>
                  <td></td>
                </tr>
              </table>
            </td>
          </tr>
        </table>
      </body>
    </html>
  </xsl:template>
</xsl:stylesheet>
