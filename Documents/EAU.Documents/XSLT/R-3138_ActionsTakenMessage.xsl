<xsl:stylesheet version="1.0" xmlns:ATM="http://ereg.egov.bg/segment/R-3138"
				        xmlns:NM="http://ereg.egov.bg/segment/0009-000005"
                xmlns:ESPBD="http://ereg.egov.bg/segment/0009-000002"
                xmlns:CFA="http://ereg.egov.bg/segment/0009-000044"
                xmlns:DU="http://ereg.egov.bg/segment/0009-000001"
                xmlns:E="http://ereg.egov.bg/segment/0009-000013"
                xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
                xmlns:xslExtension="urn:XSLExtension"
                xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
				        xmlns:ms="urn:schemas-microsoft-com:xslt" xsi:type="xsl:transform" >

  <xsl:include href="./SignatureBaseTemplates.xslt"/>
  <xsl:param name="SignatureXML"></xsl:param>

  <xsl:output omit-xml-declaration="yes" method="html"/>
  <xsl:template match="ATM:ActionsTakenMessage">
    <html>
      <xsl:call-template name="SignatureHead"/>
      <body>
        <table align="center" cellpadding="5" width= "700px">
          <tbody>
            <tr> 
              <th colspan="2">
                <h2>
                  <p align="center" class="uppercase">
                    <xsl:value-of select="ATM:ElectronicServiceProviderBasicData/ESPBD:EntityBasicData/E:Name" />
                  </p>
                </h2>
              </th>
            </tr>
            <tr>
              <td colspan ="2">
                <h3>
                  Рег. №&#160;
                  <xsl:value-of select="ATM:DocumentURI/DU:RegisterIndex" />-<xsl:value-of select="ATM:DocumentURI/DU:SequenceNumber" />-<xsl:choose>
                    <xsl:when test="ATM:DocumentURI/DU:ReceiptOrSigningDate">
                      <xsl:value-of select="xslExtension:FormatDate(ATM:DocumentURI/DU:ReceiptOrSigningDate, 'dd.MM.yyyy')"/>
                    </xsl:when>
                  </xsl:choose>
                </h3>
              </td>
            </tr>
            <tr>
              <th colspan="2">
                <p align="center">
                  <h3>
                    &#160;<xsl:value-of select="ATM:ActionsTakenMessageHeader" />
                  </h3>
                </p>
              </th>
            </tr>
            <tr>
              <td colspan ="2">
                <xsl:value-of select="ATM:ActionsTakenMessageMessage" disable-output-escaping="yes" />
              </td>
            </tr>
            <tr>
              <td align="left" width="50%">
                Дата:
                <xsl:choose>
                  <xsl:when test="ATM:DocumentReceiptOrSigningDate">
                    <xsl:value-of select="xslExtension:FormatDate(ATM:DocumentReceiptOrSigningDate, 'dd.MM.yyyy')"/>г.
                  </xsl:when>
                </xsl:choose>
              </td>
              <td width="50%">
                <xsl:call-template name="DocumentSignatures">
                  <xsl:with-param name="Signatures" select = "$SignatureXML/DocumentSignatures" />
                </xsl:call-template>
              </td>
            </tr>
          </tbody>
        </table>
      </body>
    </html>
  </xsl:template>
</xsl:stylesheet>
