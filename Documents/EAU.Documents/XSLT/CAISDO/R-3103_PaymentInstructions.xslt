<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0"
                xmlns:PI="http://ereg.egov.bg/segment/R-3103"
                xmlns:ACU="http://ereg.egov.bg/segment/0009-000044"
                xmlns:DU="http://ereg.egov.bg/segment/0009-000001"
                xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
                xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
                xmlns:xslExtension="urn:XSLExtension"
                xmlns:ms="urn:schemas-microsoft-com:xslt"
                xsi:type="xsl:transform">
  <xsl:output omit-xml-declaration="yes" method="html"/>

  <xsl:template match="PI:PaymentInstructions">
    <xsl:text disable-output-escaping='yes'>&lt;!DOCTYPE html&gt;</xsl:text>
    <html>
      <body>
        <table align="center" cellpadding="5" width= "700px" >
          <tr>
            <td colspan="2">
              <center>
                <b>
                  <xsl:value-of select="PI:DocumentTypeName" />
                </b>
              </center>
            </td>
          </tr>
          <tr>
            <td colspan="2">
              №:<xsl:value-of select="PI:DocumentURI/DU:RegisterIndex" />
              -<xsl:value-of select="PI:DocumentURI/DU:SequenceNumber" />
              -<xsl:value-of select="ms:format-date(PI:DocumentURI/DU:ReceiptOrSigningDate, 'dd.MM.yyyy')"/>
            </td>
          </tr>


          <tr>
            <td>
              <b>
                <xsl:value-of select="PI:PaymentInstructionsHeader" />
              </b>
              <br/>
            </td>
          </tr>
          <tr>
            <td>
              Сума: <xsl:value-of select="PI:Amount" /> <xsl:text>&#160;</xsl:text> <xsl:value-of select="PI:AmountCurrency" />
              <br/>
              Основание: <xsl:value-of select="PI:PaymentReason" />
              <br/>


              Краен срок на плащане:
              <xsl:variable name="deadlineDays" select="xslExtension:ExtractFirstGroup(PI:DeadlineForPayment/., '(\d*)D')"/>
              <xsl:if test="$deadlineDays and $deadlineDays != '0'">
                <xsl:value-of select="$deadlineDays"/>
                <xsl:value-of select="' '" />
                <xsl:if test="$deadlineDays = '1'">
                  <xsl:value-of select="'ден'"/>
                </xsl:if>
                <xsl:if test="$deadlineDays != '1'">
                  <xsl:value-of select="'дни'"/>
                </xsl:if>.
              </xsl:if>

              <xsl:variable name="deadlineHours" select="xslExtension:ExtractFirstGroup(PI:DeadlineForPayment/., '(\d*)H')"/>
              <xsl:if test="$deadlineHours and $deadlineHours != '0'">
                <xsl:value-of select="$deadlineHours"/>
                <xsl:value-of select="' '" />
                <xsl:value-of select="'час/а.'"/>
              </xsl:if>
              <br/>
              Съобщение за краен срок: <xsl:value-of select="PI:DeadlineMessage" />
              <br/>
              Банка: <xsl:value-of select="PI:BankName" />
              <br/>
              BIC на банка: <xsl:value-of select="PI:BIC" />
              <br/>
              IBAN на сметка получател: <xsl:value-of select="PI:IBAN" />
              <br/>
            </td>
          </tr>


        </table>
      </body>
    </html>
  </xsl:template>
</xsl:stylesheet>
