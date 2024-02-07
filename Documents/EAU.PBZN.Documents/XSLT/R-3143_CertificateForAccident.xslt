<xsl:stylesheet version="1.0" xmlns:CFA="http://ereg.egov.bg/segment/R-3143"
                xmlns:EASH="http://ereg.egov.bg/segment/0009-000152"
								xmlns:ESA="http://ereg.egov.bg/segment/0009-000016"
								xmlns:REC="http://ereg.egov.bg/segment/0009-000015"
								xmlns:ESPBD="http://ereg.egov.bg/segment/0009-000002"
								xmlns:P="http://ereg.egov.bg/segment/0009-000008"
								xmlns:NM="http://ereg.egov.bg/segment/0009-000005"
								xmlns:ID="http://ereg.egov.bg/segment/0009-000006"
								xmlns:IDBD="http://ereg.egov.bg/segment/0009-000099"
								xmlns:PA="http://ereg.egov.bg/segment/0009-000094"
								xmlns:PI="http://ereg.egov.bg/segment/R-3015"
								xmlns:AUT="http://ereg.egov.bg/segment/0009-000012"
								xmlns:DBIF="http://ereg.egov.bg/segment/R-3041"
								xmlns:IBDIP="http://ereg.egov.bg/segment/R-3033"
								xmlns:OICIBID="http://ereg.egov.bg/value/R-3034"
								xmlns:DMST="http://ereg.egov.bg/segment/R-3040"
								xmlns:SARD="http://ereg.egov.bg/segment/0009-000141"
								xmlns:EASF="http://ereg.egov.bg/segment/0009-000153"
								xmlns:E="http://ereg.egov.bg/segment/0009-000013"
								xmlns:CADR="http://ereg.egov.bg/segment/0009-000241"
                xmlns:AM="http://ereg.egov.bg/segment/R-3119"
								xmlns:PD="http://ereg.egov.bg/segment/R-3037"
                xmlns:DU="http://ereg.egov.bg/segment/0009-000001"
                xmlns:ESPD="http://ereg.egov.bg/segment/0009-000002"
                xmlns:EBD="http://ereg.egov.bg/segment/0009-000013"
                xmlns:IPD="http://ereg.egov.bg/segment/R-3037"
                xmlns:ds="http://www.w3.org/2000/09/xmldsig#"
                xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
                xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
                xmlns:xslExtension="urn:XSLExtension"
				
xmlns:ms="urn:schemas-microsoft-com:xslt" xsi:type="xsl:transform" >
  <xsl:include href="./PBZNBaseTemplates.xslt"/>
  <xsl:param name="SignatureXML"></xsl:param>

  <xsl:output omit-xml-declaration="yes" method="html"/>
  <xsl:template match="CFA:CertificateForAccident">
    <xsl:text disable-output-escaping='yes'>&lt;!DOCTYPE html&gt;</xsl:text>
    <html>
      <xsl:call-template name="Head"/>
      <body>


        <table align="center" cellpadding="5" width= "700px">
          <thead>
            <tr>
              <th colspan ="2">
                <h2 class="uppercase">
                  <b>
                    <xsl:value-of select="CFA:ElectronicServiceProviderBasicData/ESPD:EntityBasicData/EBD:Name" />
                  </b>
                </h2>
              </th>
            </tr>
            <tr>
              <th colspan ="2">
                <h2>
                  <xsl:value-of select="CFA:IssuingPoliceDepartment/IPD:PoliceDepartmentName" />
                </h2>
              </th>
            </tr>
            <tr>
              <th colspan="2">
                <hr width="80%"/>
              </th>
            </tr>
            <tr>
              <td colspan ="2">
                <h3>
                  Рег. №&#160;
                  <xsl:value-of select="CFA:DocumentURI/DU:RegisterIndex" />
                  -<xsl:value-of select="CFA:DocumentURI/DU:SequenceNumber" />
                  -<xsl:choose>
                    <xsl:when test="CFA:DocumentURI/DU:ReceiptOrSigningDate">
                      <xsl:value-of select="xslExtension:FormatDate(CFA:DocumentURI/DU:ReceiptOrSigningDate, 'dd.MM.yyyy')"/>
                    </xsl:when>
                  </xsl:choose>
                </h3>
              </td>
            </tr>

            <tr >
              <th colspan = "2">
                &#160;
              </th>
            </tr>
            <tr>
              <th colspan = "2">
                <p align="center">
                  <h1>
                    &#160;<xsl:value-of select="CFA:CertificateForAccidentHeader" />
                  </h1>
                </p>
              </th>
            </tr>
            <tr >
              <th colspan = "2">
                &#160;
              </th>
            </tr>
          </thead>
          <tbody>
            <tr >
              <td  colspan = "2">
                &#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;Настоящият документ се издава на&#160;
                <xsl:choose>
                  <xsl:when test="CFA:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Entity">
                    <xsl:value-of  select="CFA:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Entity/E:Name"/>&#160;
                  </xsl:when>
                  <xsl:otherwise>
                    <xsl:value-of  select="CFA:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Person/P:Names/NM:First"/>&#160;
                    <xsl:value-of  select="CFA:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Person/P:Names/NM:Middle"/>&#160;
                    <xsl:value-of  select="CFA:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Person/P:Names/NM:Last"/>&#160;
                  </xsl:otherwise>
                </xsl:choose>
              </td>
            </tr>
            <tr >
              <td colspan = "2">
                <xsl:choose>
                  <xsl:when test="CFA:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Entity">
                    с ЕИК/БУЛСТАТ:&#160;<xsl:value-of  select="CFA:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Entity/E:Identifier"/>&#160;
                  </xsl:when>
                </xsl:choose>
                <xsl:choose>
                  <xsl:when test="CFA:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Person">
                    с ЕГН/ЛНЧ/ЛН:&#160;
                    <xsl:value-of  select="CFA:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Person/P:Identifier"/>&#160;
                  </xsl:when>
                </xsl:choose>
                в уверение на това, че
              </td>
            </tr>
            <tr>
              <td colspan = "2">
                <xsl:value-of select="CFA:CertificateForAccidentData" disable-output-escaping="yes"/>
              </td>
            </tr>

            <tr >
              <td colspan = "2">
                &#160;
              </td>
            </tr>

            <tr>
              <td align="left" width="50%">
                Дата:
                <xsl:choose>
                  <xsl:when test="CFA:DocumentReceiptOrSigningDate">
                    <xsl:value-of select="xslExtension:FormatDate(CFA:DocumentReceiptOrSigningDate, 'dd.MM.yyyy')"/>г.
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
              <td >
                &#160;
              </td>
              <td  align="right">
                <b>
                  <xsl:value-of select="CFA:Official/CFA:PersonNames/NM:First/." />&#160;<xsl:value-of select="CFA:Official/CFA:PersonNames/NM:Last/." />
                </b>
              </td>
            </tr>

          </tbody>
        </table>
      </body>
    </html>
  </xsl:template>
</xsl:stylesheet>