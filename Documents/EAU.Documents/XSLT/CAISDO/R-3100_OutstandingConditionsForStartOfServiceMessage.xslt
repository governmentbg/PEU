<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0"
                xmlns:OSCFSODM="http://ereg.egov.bg/segment/R-3100"
                xmlns:ACU="http://ereg.egov.bg/segment/0009-000044"
                xmlns:DU="http://ereg.egov.bg/segment/0009-000001"

                xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
                xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
                xmlns:ms="urn:schemas-microsoft-com:xslt"
                xsi:type="xsl:transform">
  <xsl:output omit-xml-declaration="yes" method="html"/>

  <xsl:template match="OSCFSODM:OutstandingConditionsForStartOfServiceMessage">
    <xsl:text disable-output-escaping='yes'>&lt;!DOCTYPE html&gt;</xsl:text>
    <html>
      <body>
        <table align="center" cellpadding="5" width= "700px" >
          <tr>
            <td colspan="2">
              <center>
                <b>
                  <xsl:value-of select="OSCFSODM:DocumentTypeName" />
                </b>
              </center>
            </td>
          </tr>
          <tr>
            <td colspan="2">
              №:<xsl:value-of select="OSCFSODM:DocumentURI/DU:RegisterIndex" />
              -<xsl:value-of select="OSCFSODM:DocumentURI/DU:SequenceNumber" />
              -<xsl:value-of select="ms:format-date(OSCFSODM:DocumentURI/DU:ReceiptOrSigningDate, 'dd.MM.yyyy')"/>
              <br/>
              Дата:
              <xsl:choose>
                <xsl:when test="OSCFSODM:DocumentURI/DU:ReceiptOrSigningDate">
                  <xsl:value-of select="ms:format-date(OSCFSODM:DocumentURI/DU:ReceiptOrSigningDate, 'dd.MM.yyyy')"/>
                </xsl:when>
              </xsl:choose>
            </td>
          </tr>

          <tr>

            <td>
              <b>Неизпълнени условия за стартиране на услугата </b>
              <ul>
                <li>
                  <xsl:value-of select="OSCFSODM:Grounds" />
                </li>
              </ul>
            </td>
          </tr>


        </table>
      </body>
    </html>
  </xsl:template>
</xsl:stylesheet>
