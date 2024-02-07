<xsl:stylesheet version="1.0" xmlns:CAEFRIID="http://ereg.egov.bg/segment/R-3125"
                xmlns:ESPD="http://ereg.egov.bg/segment/0009-000002"
								xmlns:ESA="http://ereg.egov.bg/segment/0009-000016"
								xmlns:EBD="http://ereg.egov.bg/segment/0009-000013"
								xmlns:REC="http://ereg.egov.bg/segment/0009-000015"
								xmlns:P="http://ereg.egov.bg/segment/0009-000008"
                xmlns:NM="http://ereg.egov.bg/segment/0009-000005"
								xmlns:ID="http://ereg.egov.bg/segment/0009-000006"
								xmlns:DMST="http://ereg.egov.bg/segment/R-3040"
								xmlns:IPD="http://ereg.egov.bg/segment/R-3037"
                xmlns:ds="http://www.w3.org/2000/09/xmldsig#"
                xmlns:CURI="http://ereg.egov.bg/segment/0009-000044"
                xmlns:DURI="http://ereg.egov.bg/segment/0009-000001"
                xmlns:DU="http://ereg.egov.bg/segment/0009-000001"
                xmlns:xslExtension="urn:XSLExtension"
                xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
                xmlns:xsl="http://www.w3.org/1999/XSL/Transform"

xmlns:ms="urn:schemas-microsoft-com:xslt" xsi:type="xsl:transform" >
  <xsl:include href="./KATBaseTemplates.xslt"/>
  <xsl:param name="SignatureXML"></xsl:param>
  <xsl:output omit-xml-declaration="yes" method="html"/>
  <xsl:template match="CAEFRIID:CertificateAttestingToTheRightsAndPenaltiesOfVehicleDriver">
    <xsl:text disable-output-escaping='yes'>&lt;!DOCTYPE html&gt;</xsl:text>
    <html>
      <xsl:call-template name="Head"/>
      <body>
        <table align="center" cellpadding="5" width= "700px" >
          <tr>
            <th colspan ="2">
              <h3 class="uppercase">
                <xsl:value-of select="CAEFRIID:ElectronicServiceProviderBasicData/ESPD:EntityBasicData/EBD:Name" />
              </h3>
              <h3>
                <xsl:value-of select="CAEFRIID:IssuingPoliceDepartment/IPD:PoliceDepartmentName" />
                <hr></hr>
              </h3>                            
            </th>
          </tr>
          <tr>
            <th colspan ="2">
              <h2>
                УДОСТОВЕРЕНИЕ №:
                <xsl:value-of select="CAEFRIID:DocumentURI/DU:RegisterIndex" />
                -<xsl:value-of select="CAEFRIID:DocumentURI/DU:SequenceNumber" />
                -<xsl:value-of  select="ms:format-date(CAEFRIID:DocumentURI/DU:ReceiptOrSigningDate, 'dd.MM.yyyy')"/>
              </h2>
              <h2>
                По преписка с №: <xsl:value-of select="CAEFRIID:AISCaseURI/CURI:DocumentURI/DURI:RegisterIndex" />
                -<xsl:value-of select="CAEFRIID:AISCaseURI/CURI:DocumentURI/DURI:SequenceNumber" />
                -<xsl:value-of  select="ms:format-date(CAEFRIID:AISCaseURI/CURI:DocumentURI/DURI:ReceiptOrSigningDate , 'dd.MM.yyyy')"/>
              </h2>
            </th>
          </tr>
          <tr>
            <td colspan ="2">
              <xsl:value-of select="CAEFRIID:CertificateData" disable-output-escaping="yes" />
            </td>
          </tr>
          <tr>
            <td colspan ="2">
              Настоящето да послужи&#160;<xsl:choose>
                <xsl:when test="CAEFRIID:ANDCertificateReason = '2001'">
                  за Служебна бележка
                </xsl:when>
              </xsl:choose>
              <xsl:choose>
                <xsl:when test="CAEFRIID:ANDCertificateReason = '2002'">
                  за Постъпване на работа
                </xsl:when>
              </xsl:choose>
              <xsl:choose>
                <xsl:when test="CAEFRIID:ANDCertificateReason = '2003'">
                  пред	Застрахователя
                </xsl:when>
              </xsl:choose>
              <xsl:choose>
                <xsl:when test="CAEFRIID:ANDCertificateReason = '2004'">
                  пред	Медицинските органи
                </xsl:when>
              </xsl:choose>
              <xsl:choose>
                <xsl:when test="CAEFRIID:ANDCertificateReason = '2005'">
                  пред	Съдебните власти
                </xsl:when>
              </xsl:choose>
              <xsl:choose>
                <xsl:when test="CAEFRIID:ANDCertificateReason = '2006'">
                  пред  Консултски отдел
                </xsl:when>
              </xsl:choose>
              <xsl:choose>
                <xsl:when test="CAEFRIID:ANDCertificateReason = '2007'">
                  за	Лична информация
                </xsl:when>
              </xsl:choose>
              <xsl:choose>
                <xsl:when test="CAEFRIID:ANDCertificateReason = '2008'">
                  за Преквалификация
                </xsl:when>
              </xsl:choose>
            </td>
          </tr>
          <tr>
            <td colspan ="2">
              <p>
                Данните в удостоверението са актуални към&#160;
                <xsl:choose>
                  <xsl:when test="CAEFRIID:ReportDate">
                    <xsl:value-of select="xslExtension:FormatDate(CAEFRIID:ReportDate, 'dd.MM.yyyy')"/>&#160;г.&#160;
                    <xsl:value-of select="xslExtension:FormatDate(CAEFRIID:ReportDate, 'HH:mm:ss')"/>&#160;ч.&#160;
                  </xsl:when>
                </xsl:choose>
              </p>
            </td>
          </tr>
          <tr>
            <td width="50%">
              Дата:
              <xsl:choose>
                <xsl:when test="CAEFRIID:DocumentReceiptOrSigningDate">
                  <xsl:value-of  select="ms:format-date(CAEFRIID:DocumentReceiptOrSigningDate , 'dd.MM.yyyy')"/>&#160;г.
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
              <xsl:value-of select="CAEFRIID:Official/CAEFRIID:PersonNames/NM:First/." />&#160;<xsl:value-of select="CAEFRIID:Official/CAEFRIID:PersonNames/NM:Last/." />
            </td>
          </tr>
        </table>

      </body>
    </html>
  </xsl:template>
</xsl:stylesheet>
