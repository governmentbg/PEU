<xsl:stylesheet version="1.0" xmlns:RF="http://ereg.egov.bg/segment/R-3137"
				        xmlns:NM="http://ereg.egov.bg/segment/0009-000005"
                xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
                xmlns:xslExtension="urn:XSLExtension"
                xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
				
xmlns:ms="urn:schemas-microsoft-com:xslt" xsi:type="xsl:transform" >

  <xsl:include href="./SignatureBaseTemplates.xslt"/>

  <xsl:output omit-xml-declaration="yes" method="html"/>
  <xsl:template match="RF:Refusal">
    <html>
      <xsl:call-template name="SignatureHead"/>
      <body>
        <table align="center" cellpadding="5" width= "700px">
          <tbody>
            <tr>
              <td colspan ="2">
                <xsl:value-of select="RF:RefusalContent" disable-output-escaping="yes" />
              </td>
            </tr>
            <tr>
              <td width="50%">
                Дата:
                <xsl:choose>
                  <xsl:when test="RF:DocumentReceiptOrSigningDate">
                    <xsl:value-of  select="ms:format-date(RF:DocumentReceiptOrSigningDate , 'dd.MM.yyyy')"/>&#160;г.
                  </xsl:when>
                </xsl:choose>
              </td>
              <td width="50%">
              </td>
            </tr>
            <tr>
              <td width="50%">
                &#160;
              </td>
              <td width="50%">
                <xsl:value-of select="RF:Official/RF:PersonNames/NM:First/." />&#160;<xsl:value-of select="RF:Official/RF:PersonNames/NM:Last/." />
              </td>
            </tr>
          </tbody>
        </table>
      </body>
    </html>
  </xsl:template>
</xsl:stylesheet>
