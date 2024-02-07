<xsl:stylesheet version="1.0" xmlns:RAIDDL="http://ereg.egov.bg/segment/R-3045"
                xmlns:ds="http://www.w3.org/2000/09/xmldsig#"
                xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
                xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
                xmlns:PI="http://ereg.egov.bg/segment/R-3005"
                xmlns:ID="http://ereg.egov.bg/segment/R-3004"
                xmlns:P="http://ereg.egov.bg/segment/0009-000008"
                xmlns:AUT="http://ereg.egov.bg/segment/0009-000012"
                xmlns:NM="http://ereg.egov.bg/segment/0009-000005"
                xmlns:NMLN="http://ereg.egov.bg/segment/R-3003"
                xmlns:ADR="http://ereg.egov.bg/segment/0009-000094"
                xmlns:GENDER="http://ereg.egov.bg/segment/0009-000156"
                xmlns:SPOUSE="http://ereg.egov.bg/segment/0009-000135"
                xmlns:SPDATA="http://ereg.egov.bg/segment/0009-000008"
                xmlns:SPNM ="http://ereg.egov.bg/segment/0009-000005"
                xmlns:CRBD="http://ereg.egov.bg/segment/0009-000110"
                xmlns:ESA="http://ereg.egov.bg/segment/0009-000016"
                xmlns:REC="http://ereg.egov.bg/segment/0009-000015"
				        xmlns:IPAS="http://ereg.egov.bg/segment/R-3043"
				        xmlns:EASH="http://ereg.egov.bg/segment/0009-000152"
				        xmlns:PDC="http://ereg.egov.bg/segment/R-3037"
				        xmlns:DURI="http://ereg.egov.bg/segment/0009-000001"
				        xmlns:CN="http://ereg.egov.bg/segment/R-3020"
				        xmlns:CH="http://ereg.egov.bg/segment/0009-000133"
				        xmlns:IDBD="http://ereg.egov.bg/segment/0009-000099"
                xmlns:EASF="http://ereg.egov.bg/segment/0009-000153"
                xmlns:xslExtension="urn:XSLExtension"
                xmlns:ms="urn:schemas-microsoft-com:xslt" xsi:type="xsl:transform" >

  <xsl:include href="./KATBaseTemplates.xslt"/>
  <xsl:output omit-xml-declaration="yes" method="html"/>
  <xsl:template match="RAIDDL:RequestForApplicationForIssuingDuplicateDrivingLicense">
    <xsl:text disable-output-escaping='yes'>&lt;!DOCTYPE html&gt;</xsl:text>
    <html>
      <xsl:call-template name="Head"/>
      <body>
        <table width="100%" align="center" border="0">
          <thead width="100%">

            <tr style="border: none;">
              <th align="center">
                &#160;
              </th>
            </tr>
            <tr style="border: none;">
              <th align="center">
                <xsl:value-of  select="RAIDDL:ElectronicAdministrativeServiceHeader/EASH:DocumentTypeName"/>
              </th>
            </tr>
            <tr style="border: none;">
              <th align="center">
                &#160;
              </th>
            </tr>
          </thead>
          <tbody width="100%">
            <tr>
              <td align="center">
                &#160;
              </td>
            </tr>
            <tr>
              <td align="center">
                От: &#160;
                <xsl:value-of  select="RAIDDL:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Person/P:Names/NM:First/."/>
                &#160;
                <xsl:value-of  select="RAIDDL:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Person/P:Names/NM:Middle/."/>
                &#160;
                <xsl:value-of  select="RAIDDL:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Person/P:Names/NM:Last/."/>

              </td>
            </tr>
            <tr>
              <td align="center">
                &#160;
              </td>
            </tr>
            <tr>
              <td align="center">
                с ЕГН &#160; <xsl:value-of  select="RAIDDL:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Person/P:Identifier/."/>
                ,&#160; E-Mail:<xsl:value-of  select="RAIDDL:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:EmailAddress"/>
              </td>
            </tr>

            <tr>
              <td width="50%">
                Дата:&#160;<xsl:value-of  select="ms:format-date(RAIDDL:ElectronicAdministrativeServiceFooter/EASF:ApplicationSigningTime , 'dd.MM.yyyy')"/>г.
              </td>
              <td width="50%">

              </td>
            </tr>
          </tbody>
        </table>
      </body>
    </html>
  </xsl:template>

</xsl:stylesheet>