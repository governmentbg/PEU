<xsl:stylesheet version="1.0" xmlns:I="http://ereg.egov.bg/segment/R-3254"
				        xmlns:NM="http://ereg.egov.bg/segment/0009-000005"
                xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
                xmlns:xslExtension="urn:XSLExtension"
                xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
				
xmlns:ms="urn:schemas-microsoft-com:xslt" xsi:type="xsl:transform" >

  <xsl:include href="./BDSBaseTemplates.xslt"/>
  <xsl:param name="SignatureXML"></xsl:param>

  <xsl:output omit-xml-declaration="yes" method="html"/>
  <xsl:template match="I:InvitationToDrawUpAUAN">
    <html>
      <xsl:call-template name="Head"/>
      <body>
		  <table align="center" cellpadding="5" width="700px">
			  <tbody>
				  <tr>
					  <td colspan="2">
						  <xsl:value-of select="I:Content" disable-output-escaping="yes" />
					  </td>
				  </tr>
				  <tr>
					  <td width="50%">
						  Дата:
						  <xsl:choose>
							  <xsl:when test="I:DocumentReceiptOrSigningDate">
								  <xsl:value-of  select="ms:format-date(I:DocumentReceiptOrSigningDate , 'dd.MM.yyyy')"/>&#160;г.
							  </xsl:when>
						  </xsl:choose>
					  </td>
					  <td width="50%">
						  <xsl:call-template name="DocumentSignatures">
							  <xsl:with-param name="Signatures" select = "$SignatureXML/DocumentSignatures" />
						  </xsl:call-template>
					  </td>
				  </tr>
				  <tr>
					  <td width="50%">
						  &#160;
					  </td>
					  <td width="50%">
						  <xsl:value-of select="I:Official/I:Position" />&#160;<xsl:value-of select="I:Official/I:PersonNames/NM:First/." />&#160;<xsl:value-of select="I:Official/I:PersonNames/NM:Last/." />
					  </td>
				  </tr>
			  </tbody>
		  </table>
      </body>
    </html>
  </xsl:template>
</xsl:stylesheet>
