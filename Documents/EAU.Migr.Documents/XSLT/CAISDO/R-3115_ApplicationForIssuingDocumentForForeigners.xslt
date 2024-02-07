<xsl:stylesheet version="1.0" xmlns:AFIDFF="http://ereg.egov.bg/segment/R-3115"
                xmlns:EASH="http://ereg.egov.bg/segment/0009-000152"
				        xmlns:ESA="http://ereg.egov.bg/segment/0009-000016"
				        xmlns:REC="http://ereg.egov.bg/segment/0009-000015"
				        xmlns:P="http://ereg.egov.bg/segment/0009-000008"
				        xmlns:NM="http://ereg.egov.bg/segment/0009-000005"
				        xmlns:ID="http://ereg.egov.bg/segment/0009-000006"
				        xmlns:IDBD="http://ereg.egov.bg/segment/0009-000099"
				        xmlns:PA="http://ereg.egov.bg/segment/0009-000094"
				        xmlns:AFIDFFD="http://ereg.egov.bg/segment/R-3116"
				        xmlns:PI="http://ereg.egov.bg/segment/R-3015"
				        xmlns:AUT="http://ereg.egov.bg/segment/0009-000012"
				        xmlns:DBIF="http://ereg.egov.bg/segment/R-3041"
				        xmlns:IBDIP="http://ereg.egov.bg/segment/R-3033"
				        xmlns:OICIBID="http://ereg.egov.bg/value/R-3034"
				        xmlns:DMST="http://ereg.egov.bg/segment/R-3040"
				        xmlns:SARD="http://ereg.egov.bg/segment/0009-000141"
				        xmlns:EASF="http://ereg.egov.bg/segment/0009-000153"
				        xmlns:CSHP="http://ereg.egov.bg/segment/0009-000133"
                xmlns:DECL="http://ereg.egov.bg/segment//R-3136"
                xmlns:ds="http://www.w3.org/2000/09/xmldsig#"
                xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
                xmlns:ADD="http://ereg.egov.bg/segment/0009-000139"
                xmlns:xslExtension="urn:XSLExtension"
                xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
				
xmlns:ms="urn:schemas-microsoft-com:xslt" xsi:type="xsl:transform" >
  <xsl:include href="./MIGRBaseTemplates.xslt"/>

  <xsl:output omit-xml-declaration="yes" method="html"/>
  <xsl:template match="AFIDFF:ApplicationForIssuingDocumentForForeigners">
    <xsl:text disable-output-escaping='yes'>&lt;!DOCTYPE html&gt;</xsl:text>
    <html>
      <xsl:call-template name="Head"/>

      <body>
        <table align="center" cellpadding="5" width= "700px">
          <thead>
            <tr>
              <th colspan ="2">
                &#160;
              </th>
            </tr>
            <tr>
              <th align="center" colspan ="2">
                <h2>ЗАЯВЛЕНИЕ</h2>
              </th>
            </tr>
          </thead>
          <tbody>           
            <tr>
              <td colspan ="2">
                &#160;
              </td>
            </tr>

            <tr>
              <td colspan ="2" >
                <xsl:choose>
                  <xsl:when test="AFIDFF:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Author/AUT:AuthorQualityType != 'R-1001'">
                   От
                    <xsl:value-of  select="AFIDFF:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Author/AUT:Person/P:Names/NM:First/."/>
                    &#160;
                    <xsl:choose>
                      <xsl:when test="AFIDFF:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Author/AUT:Person/P:Names/NM:Middle">
                        <xsl:value-of  select="AFIDFF:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Author/AUT:Person/P:Names/NM:Middle/."/>
                        &#160;
                      </xsl:when>
                    </xsl:choose>
                    <xsl:value-of  select="AFIDFF:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Author/AUT:Person/P:Names/NM:Last/."/>

                  </xsl:when>
                  <xsl:otherwise>
                    От
                    <xsl:value-of  select="AFIDFF:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Person/P:Names/NM:First/."/>
                    &#160;
                    <xsl:choose>
                      <xsl:when test="AFIDFF:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Person/P:Names/NM:Middle">
                        <xsl:value-of  select="AFIDFF:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Person/P:Names/NM:Middle/."/>
                        &#160;
                      </xsl:when>
                    </xsl:choose>
                    <xsl:value-of  select="AFIDFF:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Person/P:Names/NM:Last/."/>

                  </xsl:otherwise>
                </xsl:choose>             
                , ЛН/ЛНЧ/ЕГН:
                <xsl:choose>
                  <xsl:when test="AFIDFF:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Author/AUT:AuthorQualityType != 'R-1001'">
                    <xsl:value-of  select="AFIDFF:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Author/AUT:Person/P:Identifier/."/>
                  </xsl:when>
                  <xsl:otherwise>
                    <xsl:value-of  select="AFIDFF:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Person/P:Identifier/."/>
                  </xsl:otherwise>
                </xsl:choose>
              </td>

            </tr>
            <xsl:choose>
              <xsl:when test="AFIDFF:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Author/AUT:AuthorQualityType != 'R-1001'">
                <tr>
                  <td colspan ="2">
                    Удостоверението се издава за:&#160;
                    <xsl:value-of  select="AFIDFF:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Person/P:Names/NM:First/."/>
                    &#160;
                    <xsl:value-of  select="AFIDFF:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Person/P:Names/NM:Middle/."/>
                    &#160;
                    <xsl:value-of  select="AFIDFF:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Person/P:Names/NM:Last/."/>,
                    ЛН/ЛНЧ/ЕГН:
                    <xsl:value-of  select="AFIDFF:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Person/P:Identifier/."/>

                  </td>
                </tr>
              </xsl:when>
              <xsl:otherwise>
              </xsl:otherwise>
            </xsl:choose>
            <tr>
              <td colspan ="2">
                роден/а на
                <xsl:choose>
                  <xsl:when test="AFIDFF:ApplicationForIssuingDocumentForForeignersData/AFIDFFD:BirthDate">
                    <xsl:choose>
                      <xsl:when test="string-length(AFIDFF:ApplicationForIssuingDocumentForForeignersData/AFIDFFD:BirthDate)>7">
                        <xsl:value-of  select="AFIDFF:ApplicationForIssuingDocumentForForeignersData/AFIDFFD:BirthDate"/> г.
                      </xsl:when>
                      <xsl:otherwise>
                        <xsl:choose>
                          <xsl:when test="string-length(AFIDFF:ApplicationForIssuingDocumentForForeignersData/AFIDFFD:BirthDate)>4">
                            <xsl:value-of  select="AFIDFF:ApplicationForIssuingDocumentForForeignersData/AFIDFFD:BirthDate"/> г.
                          </xsl:when>
                          <xsl:otherwise>
                            <xsl:value-of  select="AFIDFF:ApplicationForIssuingDocumentForForeignersData/AFIDFFD:BirthDate"/> г.
                          </xsl:otherwise>
                        </xsl:choose>
                      </xsl:otherwise>
                    </xsl:choose>

                  </xsl:when>
                </xsl:choose>             
                , гражданин/ка на
                <xsl:value-of  select="AFIDFF:ApplicationForIssuingDocumentForForeignersData/AFIDFFD:Citizenship/CSHP:CountryName"/>

              </td>

            </tr>
            <tr>
              <td colspan ="2">
                Aдрес:
                <xsl:choose>
                  <xsl:when test="AFIDFF:ApplicationForIssuingDocumentForForeignersData/AFIDFFD:Address/PA:DistrictGRAOName">
                    Обл. <xsl:value-of  select="AFIDFF:ApplicationForIssuingDocumentForForeignersData/AFIDFFD:Address/PA:DistrictGRAOName"/>
                  </xsl:when>
                </xsl:choose>
                <xsl:choose>
                  <xsl:when test="AFIDFF:ApplicationForIssuingDocumentForForeignersData/AFIDFFD:Address/PA:MunicipalityGRAOName">
                    Общ. <xsl:value-of  select="AFIDFF:ApplicationForIssuingDocumentForForeignersData/AFIDFFD:Address/PA:MunicipalityGRAOName"/>
                  </xsl:when>
                </xsl:choose>
                <xsl:choose>
                  <xsl:when test="AFIDFF:ApplicationForIssuingDocumentForForeignersData/AFIDFFD:Address/PA:DistrictGRAOName">
                    гр./с. <xsl:value-of  select="AFIDFF:ApplicationForIssuingDocumentForForeignersData/AFIDFFD:Address/PA:SettlementGRAOName"/><br/>
                  </xsl:when>
                </xsl:choose>
                <xsl:choose>
                  <xsl:when test="AFIDFF:ApplicationForIssuingDocumentForForeignersData/AFIDFFD:Address/PA:StreetText">
                    ул. <xsl:value-of  select="AFIDFF:ApplicationForIssuingDocumentForForeignersData/AFIDFFD:Address/PA:StreetText"/>
                  </xsl:when>
                </xsl:choose>
                <xsl:choose>
                  <xsl:when test="AFIDFF:ApplicationForIssuingDocumentForForeignersData/AFIDFFD:Address/PA:BuildingNumber">
                    № <xsl:value-of  select="AFIDFF:ApplicationForIssuingDocumentForForeignersData/AFIDFFD:Address/PA:BuildingNumber"/>
                  </xsl:when>
                </xsl:choose>
                <xsl:choose>
                  <xsl:when test="AFIDFF:ApplicationForIssuingDocumentForForeignersData/AFIDFFD:Address/PA:Entrance">
                    вх. <xsl:value-of  select="AFIDFF:ApplicationForIssuingDocumentForForeignersData/AFIDFFD:Address/PA:Entrance"/>
                  </xsl:when>
                </xsl:choose>
                <xsl:choose>
                  <xsl:when test="AFIDFF:ApplicationForIssuingDocumentForForeignersData/AFIDFFD:Address/PA:Floor">
                    ет. <xsl:value-of  select="AFIDFF:ApplicationForIssuingDocumentForForeignersData/AFIDFFD:Address/PA:Floor"/>
                  </xsl:when>
                </xsl:choose>
                <xsl:choose>
                  <xsl:when test="AFIDFF:ApplicationForIssuingDocumentForForeignersData/AFIDFFD:Address/PA:Apartment">
                    ап. <xsl:value-of  select="AFIDFF:ApplicationForIssuingDocumentForForeignersData/AFIDFFD:Address/PA:Apartment"/>
                  </xsl:when>
                </xsl:choose>
              </td>
            </tr>
            <tr>
              <td colspan ="2">
                &#160;
              </td>
            </tr>
            <tr>
              <td colspan ="2" >
                <b>&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;УВАЖАЕМИ ГОСПОДИН/ГОСПОЖО ДИРЕКТОР,</b>
              </td>

            </tr>            
            <tr>
              <td colspan ="2">
                <p>
                  &#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;Моля да ми бъде издадено удостоверение за
                  <xsl:value-of  select="AFIDFF:ApplicationForIssuingDocumentForForeignersData/AFIDFFD:CertificateFor"/>.
                </p>
              </td>

            </tr>
            <tr>
              <td colspan = "2">
                Удостоверението да послужи пред
                <xsl:choose>
                  <xsl:when test="AFIDFF:ApplicationForIssuingDocumentForForeignersData/AFIDFFD:DocumentMustServeTo/DMST:InRepublicOfBulgariaDocumentMustServeTo ">
                    <xsl:value-of  select="AFIDFF:ApplicationForIssuingDocumentForForeignersData/AFIDFFD:DocumentMustServeTo/DMST:InRepublicOfBulgariaDocumentMustServeTo"/>.
                  </xsl:when>
                  <xsl:otherwise>
                    <xsl:value-of  select="AFIDFF:ApplicationForIssuingDocumentForForeignersData/AFIDFFD:DocumentMustServeTo/DMST:AbroadDocumentMustServeTo"/>.
                  </xsl:otherwise>
                </xsl:choose>
              </td>
            </tr>
            <xsl:choose>
              <xsl:when test = "AFIDFF:Declarations">
                <xsl:for-each select="AFIDFF:Declarations/AFIDFF:Declaration">
                  <xsl:choose>
                    <xsl:when test="DECL:IsDeclarationFilled = 'true'">
                      <tr>
                        <td colspan="2">
                          <xsl:value-of  select="DECL:DeclarationName" disable-output-escaping="yes" />
                        </td>
                      </tr>
                      <xsl:choose>
                        <xsl:when test="DECL:FurtherDescriptionFromDeclarer">
                          <tr>
                            <td colspan="2">
                              Декларирам (допълнително описание на обстоятелствата по декларацията):<xsl:value-of  select="DECL:FurtherDescriptionFromDeclarer"/>
                            </td>
                          </tr>
                          <tr>
                            <td colspan="2">
                              <xsl:value-of  select="DECL:FurtherDescriptionFromDeclarer"/>
                            </td>
                          </tr>
                        </xsl:when>
                      </xsl:choose>
                    </xsl:when>
                  </xsl:choose>

                </xsl:for-each>
              </xsl:when>
            </xsl:choose>

            <xsl:choose>
              <xsl:when test = "AFIDFF:AttachedDocuments">
                <tr>
                  <td colspan="2">
                    Приложени документи:
                  </td>
                </tr>
                <tr>
                  <td colspan="2">
                    <ol>
                      <xsl:for-each select="AFIDFF:AttachedDocuments/AFIDFF:AttachedDocument">
                        <li>
                          <xsl:value-of select="ADD:AttachedDocumentDescription" />
                        </li>
                      </xsl:for-each>
                    </ol>
                  </td>
                </tr>
              </xsl:when>
            </xsl:choose>

            <tr>
              <td width="50%">
                Дата:&#160;<xsl:value-of  select="ms:format-date(AFIDFF:ElectronicAdministrativeServiceFooter/EASF:ApplicationSigningTime , 'dd.MM.yyyy')"/>г.
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