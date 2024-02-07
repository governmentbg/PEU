<xsl:stylesheet version="1.0" xmlns:AFIDOI="http://ereg.egov.bg/segment/R-3123"
                xmlns:EASH="http://ereg.egov.bg/segment/0009-000152"
				        xmlns:ESA="http://ereg.egov.bg/segment/0009-000016"
				        xmlns:REC="http://ereg.egov.bg/segment/0009-000015"
				        xmlns:P="http://ereg.egov.bg/segment/0009-000008"
				        xmlns:NM="http://ereg.egov.bg/segment/0009-000005"
				        xmlns:ID="http://ereg.egov.bg/segment/0009-000006"
				        xmlns:IDBD="http://ereg.egov.bg/segment/0009-000099"
				        xmlns:PA="http://ereg.egov.bg/segment/0009-000094"
				        xmlns:AFIDOID="http://ereg.egov.bg/segment/R-3124"
				        xmlns:PI="http://ereg.egov.bg/segment/R-3015"
				        xmlns:AUT="http://ereg.egov.bg/segment/0009-000012"
				        xmlns:DBIF="http://ereg.egov.bg/segment/R-3041"
				        xmlns:IBDIP="http://ereg.egov.bg/segment/R-3033"
				        xmlns:OICIBID="http://ereg.egov.bg/value/R-3034"
				        xmlns:DMST="http://ereg.egov.bg/segment/R-3040"
				        xmlns:EASF="http://ereg.egov.bg/segment/0009-000153"
                xmlns:AA="http://ereg.egov.bg/segment/0009-000141"
				        xmlns:PD="http://ereg.egov.bg/segment/R-3037"
				        xmlns:E="http://ereg.egov.bg/segment/0009-000013"
				        xmlns:CADR="http://ereg.egov.bg/segment/R-3203"
                xmlns:DECL="http://ereg.egov.bg/segment//R-3136"
                xmlns:ADD="http://ereg.egov.bg/segment/0009-000139"
                xmlns:ds="http://www.w3.org/2000/09/xmldsig#"
                xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
                xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
                xmlns:xslExtension="urn:XSLExtension"
				
xmlns:ms="urn:schemas-microsoft-com:xslt" xsi:type="xsl:transform" >
  <xsl:include href="./PBZNBaseTemplates.xslt"/>

  <xsl:output omit-xml-declaration="yes" method="html"/>
  <xsl:template match="AFIDOI:ApplicationForIssuingDocumentsOfIncurredAndOtherAccidentsAndProvideInformationOnTheActivityCarriedOut">
    <xsl:text disable-output-escaping='yes'>&lt;!DOCTYPE html&gt;</xsl:text>
    <html>
      <xsl:call-template name="Head"/>
      <body>
        <table align="center" cellpadding="5" width= "700px">
          <thead>
            <tr>
              <th>
                &#160;
              </th>
              <th>
                <p align="right">
                  ДО<br/>
                  ДИРЕКТОРА НА<br/>
                  <xsl:value-of select="AFIDOI:IssuingPoliceDepartment/PD:PoliceDepartmentName" />
                </p>
              </th>

            </tr>
            <tr>
              <th colspan="2">
                &#160;
              </th>
            </tr>
            <tr>
              <th colspan="2">
                <h2>
                  <xsl:value-of select="AFIDOI:ElectronicAdministrativeServiceHeader/EASH:DocumentTypeName" />
                </h2>
              </th>
            </tr>
          </thead>
          <tbody>
            <tr>
              <td colspan="2">
                &#160;
              </td>
            </tr>
            <tr>
              <td colspan="2">
                &#160;
              </td>
            </tr>
            <xsl:choose>
              <xsl:when test="AFIDOI:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Author/AUT:AuthorQualityType != 'R-1001'">
                <xsl:choose>
                  <xsl:when test="AFIDOI:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Entity">
                    <tr>
                      <td colspan="2">
                        От&#160;<xsl:value-of  select="AFIDOI:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Entity/E:Name"/>
                      </td>
                    </tr>
                    <tr>
                      <td colspan="2">
                        ЕИК/БУЛСТАТ:&#160;<xsl:value-of  select="AFIDOI:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Entity/E:Identifier"/>
                        <br/>
                        Седалище и адрес на управление:&#160;
                        <xsl:choose>
                          <xsl:when test="AFIDOI:ApplicationForIssuingDocumentsOfIncurredAndOtherAccidentsAndProvideInformationOnTheActivityCarriedOutData/AFIDOID:EntityManagementAddress/CADR:DistrictName">
                            Обл.&#160;<xsl:value-of  select="AFIDOI:ApplicationForIssuingDocumentsOfIncurredAndOtherAccidentsAndProvideInformationOnTheActivityCarriedOutData/AFIDOID:EntityManagementAddress/CADR:DistrictName"/>&#160;
                          </xsl:when>
                        </xsl:choose>
                        <xsl:choose>
                          <xsl:when test="AFIDOI:ApplicationForIssuingDocumentsOfIncurredAndOtherAccidentsAndProvideInformationOnTheActivityCarriedOutData/AFIDOID:EntityManagementAddress/CADR:MunicipalityName">
                            Общ.&#160;<xsl:value-of  select="AFIDOI:ApplicationForIssuingDocumentsOfIncurredAndOtherAccidentsAndProvideInformationOnTheActivityCarriedOutData/AFIDOID:EntityManagementAddress/CADR:MunicipalityName"/>&#160;
                          </xsl:when>
                        </xsl:choose>
                        <xsl:choose>
                          <xsl:when test="AFIDOI:ApplicationForIssuingDocumentsOfIncurredAndOtherAccidentsAndProvideInformationOnTheActivityCarriedOutData/AFIDOID:EntityManagementAddress/CADR:SettlementName">
                            гр./с.&#160;<xsl:value-of  select="AFIDOI:ApplicationForIssuingDocumentsOfIncurredAndOtherAccidentsAndProvideInformationOnTheActivityCarriedOutData/AFIDOID:EntityManagementAddress/CADR:SettlementName"/>&#160;
                          </xsl:when>
                        </xsl:choose>
                        <xsl:choose>
                          <xsl:when test="AFIDOI:ApplicationForIssuingDocumentsOfIncurredAndOtherAccidentsAndProvideInformationOnTheActivityCarriedOutData/AFIDOID:EntityManagementAddress/CADR:AreaName">
                            р-н.&#160;<xsl:value-of  select="AFIDOI:ApplicationForIssuingDocumentsOfIncurredAndOtherAccidentsAndProvideInformationOnTheActivityCarriedOutData/AFIDOID:EntityManagementAddress/CADR:AreaName"/>&#160;
                          </xsl:when>
                        </xsl:choose>
                        <xsl:choose>
                          <xsl:when test="AFIDOI:ApplicationForIssuingDocumentsOfIncurredAndOtherAccidentsAndProvideInformationOnTheActivityCarriedOutData/AFIDOID:EntityManagementAddress/CADR:PostCode">
                            п.к.&#160;<xsl:value-of  select="AFIDOI:ApplicationForIssuingDocumentsOfIncurredAndOtherAccidentsAndProvideInformationOnTheActivityCarriedOutData/AFIDOID:EntityManagementAddress/CADR:PostCode"/>&#160;
                          </xsl:when>
                        </xsl:choose>
                        <xsl:choose>
                          <xsl:when test="AFIDOI:ApplicationForIssuingDocumentsOfIncurredAndOtherAccidentsAndProvideInformationOnTheActivityCarriedOutData/AFIDOID:EntityManagementAddress/CADR:HousingEstate">
                            ж.к.&#160;<xsl:value-of  select="AFIDOI:ApplicationForIssuingDocumentsOfIncurredAndOtherAccidentsAndProvideInformationOnTheActivityCarriedOutData/AFIDOID:EntityManagementAddress/CADR:HousingEstate"/>&#160;
                          </xsl:when>
                        </xsl:choose>
                        <xsl:choose>
                          <xsl:when test="AFIDOI:ApplicationForIssuingDocumentsOfIncurredAndOtherAccidentsAndProvideInformationOnTheActivityCarriedOutData/AFIDOID:EntityManagementAddress/CADR:Street">
                            бул./ул.&#160;<xsl:value-of  select="AFIDOI:ApplicationForIssuingDocumentsOfIncurredAndOtherAccidentsAndProvideInformationOnTheActivityCarriedOutData/AFIDOID:EntityManagementAddress/CADR:Street"/>&#160;
                          </xsl:when>
                        </xsl:choose>
                        <xsl:choose>
                          <xsl:when test="AFIDOI:ApplicationForIssuingDocumentsOfIncurredAndOtherAccidentsAndProvideInformationOnTheActivityCarriedOutData/AFIDOID:EntityManagementAddress/CADR:StreetNumber">
                            №&#160;<xsl:value-of  select="AFIDOI:ApplicationForIssuingDocumentsOfIncurredAndOtherAccidentsAndProvideInformationOnTheActivityCarriedOutData/AFIDOID:EntityManagementAddress/CADR:StreetNumber"/>&#160;
                          </xsl:when>
                        </xsl:choose>
                        <xsl:choose>
                          <xsl:when test="AFIDOI:ApplicationForIssuingDocumentsOfIncurredAndOtherAccidentsAndProvideInformationOnTheActivityCarriedOutData/AFIDOID:EntityManagementAddress/CADR:Block">
                            бл.&#160;<xsl:value-of  select="AFIDOI:ApplicationForIssuingDocumentsOfIncurredAndOtherAccidentsAndProvideInformationOnTheActivityCarriedOutData/AFIDOID:EntityManagementAddress/CADR:Block"/>&#160;
                          </xsl:when>
                        </xsl:choose>
                        <xsl:choose>
                          <xsl:when test="AFIDOI:ApplicationForIssuingDocumentsOfIncurredAndOtherAccidentsAndProvideInformationOnTheActivityCarriedOutData/AFIDOID:EntityManagementAddress/CADR:Entrance">
                            вх.<xsl:value-of  select="AFIDOI:ApplicationForIssuingDocumentsOfIncurredAndOtherAccidentsAndProvideInformationOnTheActivityCarriedOutData/AFIDOID:EntityManagementAddress/CADR:Entrance"/>&#160;
                          </xsl:when>
                        </xsl:choose>
                        <xsl:choose>
                          <xsl:when test="AFIDOI:ApplicationForIssuingDocumentsOfIncurredAndOtherAccidentsAndProvideInformationOnTheActivityCarriedOutData/AFIDOID:EntityManagementAddress/CADR:Floor">
                            ет.<xsl:value-of  select="AFIDOI:ApplicationForIssuingDocumentsOfIncurredAndOtherAccidentsAndProvideInformationOnTheActivityCarriedOutData/AFIDOID:EntityManagementAddress/CADR:Floor"/>&#160;
                          </xsl:when>
                        </xsl:choose>
                        <xsl:choose>
                          <xsl:when test="AFIDOI:ApplicationForIssuingDocumentsOfIncurredAndOtherAccidentsAndProvideInformationOnTheActivityCarriedOutData/AFIDOID:EntityManagementAddress/CADR:Apartment">
                            ап.&#160;<xsl:value-of  select="AFIDOI:ApplicationForIssuingDocumentsOfIncurredAndOtherAccidentsAndProvideInformationOnTheActivityCarriedOutData/AFIDOID:EntityManagementAddress/CADR:Apartment"/>&#160;
                          </xsl:when>
                        </xsl:choose>
                      </td>
                    </tr>
                  </xsl:when>
                  <xsl:otherwise>
                    <tr>
                      <td colspan="2">
                        от&#160;
                        <xsl:value-of  select="AFIDOI:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Person/P:Names/NM:First/."/>
                        &#160;
                        <xsl:choose>
                          <xsl:when test="AFIDOI:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Person/P:Names/NM:Middle/.">
                            <xsl:value-of  select="AFIDOI:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Person/P:Names/NM:Middle/."/>&#160;
                          </xsl:when>
                        </xsl:choose>
                        &#160;
                        <xsl:value-of  select="AFIDOI:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Person/P:Names/NM:Last/."/>
                      </td>
                    </tr>
                    <tr>
                      <td colspan="2">
                        ЕГН/ЛНЧ:&#160;
                        <xsl:value-of  select="AFIDOI:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Person/P:Identifier/."/>
                      </td>

                    </tr>
                  </xsl:otherwise>
                </xsl:choose>
                <tr>
                  <td colspan="2">
                    упълномощен/представляван от:&#160;
                    <xsl:value-of  select="AFIDOI:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Author/AUT:Person/P:Names/NM:First/."/>
                    &#160;
                    <xsl:choose>
                      <xsl:when test="AFIDOI:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Author/AUT:Person/P:Names/NM:Middle/.">
                        <xsl:value-of  select="AFIDOI:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Author/AUT:Person/P:Names/NM:Middle/."/>&#160;
                      </xsl:when>
                    </xsl:choose>
                    &#160;
                    <xsl:value-of  select="AFIDOI:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Author/AUT:Person/P:Names/NM:Last/."/>
                  </td >
                </tr>
                <tr>
                  <td colspan="2">
                    ЕГН/ЛНЧ:&#160;
                    <xsl:value-of  select="AFIDOI:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Author/AUT:Person/P:Identifier/."/>;
                  </td>

                </tr>
                <tr>
                  <td colspan="2">
                    <xsl:choose>
                      <xsl:when test="AFIDOI:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Author/AUT:Person/P:IdentityDocument/IDBD:IdentityDocumentType = '0006-000087'">
                        Лична карта
                      </xsl:when>
                    </xsl:choose>
                    <xsl:choose>
                      <xsl:when test="AFIDOI:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Author/AUT:Person/P:IdentityDocument/IDBD:IdentityDocumentType = '0006-000088'">
                        Паспорт
                      </xsl:when>
                    </xsl:choose>
                    <xsl:choose>
                      <xsl:when test="AFIDOI:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Author/AUT:Person/P:IdentityDocument/IDBD:IdentityDocumentType = '0006-000089'">
                        Дипломатически паспорт
                      </xsl:when>
                    </xsl:choose>
                    <xsl:choose>
                      <xsl:when test="AFIDOI:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Author/AUT:Person/P:IdentityDocument/IDBD:IdentityDocumentType = '0006-000090'">
                        Служебен паспорт
                      </xsl:when>
                    </xsl:choose>
                    <xsl:choose>
                      <xsl:when test="AFIDOI:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Author/AUT:Person/P:IdentityDocument/IDBD:IdentityDocumentType = '0006-000091'">
                        Моряшки паспорт
                      </xsl:when>
                    </xsl:choose>
                    <xsl:choose>
                      <xsl:when test="AFIDOI:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Author/AUT:Person/P:IdentityDocument/IDBD:IdentityDocumentType = '0006-000092'">
                        Военна карта за самоличност
                      </xsl:when>
                    </xsl:choose>
                    <xsl:choose>
                      <xsl:when test="AFIDOI:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Author/AUT:Person/P:IdentityDocument/IDBD:IdentityDocumentType = '0006-000093'">
                        Свидетелство за управление на моторно превозно средство
                      </xsl:when>
                    </xsl:choose>
                    <xsl:choose>
                      <xsl:when test="AFIDOI:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Author/AUT:Person/P:IdentityDocument/IDBD:IdentityDocumentType = '0006-000094'">
                        Временен паспорт
                      </xsl:when>
                    </xsl:choose>
                    <xsl:choose>
                      <xsl:when test="AFIDOI:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Author/AUT:Person/P:IdentityDocument/IDBD:IdentityDocumentType = '0006-000095'">
                        Служебен открит лист за преминаване на границата
                      </xsl:when>
                    </xsl:choose>
                    <xsl:choose>
                      <xsl:when test="AFIDOI:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Author/AUT:Person/P:IdentityDocument/IDBD:IdentityDocumentType = '0006-000097'">
                        Карта на бежанец
                      </xsl:when>
                    </xsl:choose>
                    <xsl:choose>
                      <xsl:when test="AFIDOI:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Author/AUT:Person/P:IdentityDocument/IDBD:IdentityDocumentType = '0006-000099'">
                        Карта на чужденец с хуманитарен статут
                      </xsl:when>
                    </xsl:choose>
                    <xsl:choose>
                      <xsl:when test="AFIDOI:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Author/AUT:Person/P:IdentityDocument/IDBD:IdentityDocumentType = '0006-000098'">
                        Карта на чужденец, получил убежище
                      </xsl:when>
                    </xsl:choose>
                    <xsl:choose>
                      <xsl:when test="AFIDOI:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Author/AUT:Person/P:IdentityDocument/IDBD:IdentityDocumentType = '0006-000121'">
                        Разрешение за пребиваване
                      </xsl:when>
                    </xsl:choose>
                    <xsl:choose>
                      <xsl:when test="AFIDOI:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Author/AUT:Person/P:IdentityDocument/IDBD:IdentityDocumentType = '0006-000122'">
                        Удостоверение за пребиваване на гражданин на ЕС
                      </xsl:when>
                    </xsl:choose>
                    &#160;№:&#160;<xsl:value-of  select="AFIDOI:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Author/AUT:Person/P:IdentityDocument/IDBD:IdentityNumber"/>
                    <br/>
                    изд. на:&#160;
                    <xsl:choose>
                      <xsl:when test="AFIDOI:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Author/AUT:Person/P:IdentityDocument/IDBD:IdentitityIssueDate">
                        <xsl:value-of select="xslExtension:FormatDate(AFIDOI:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Author/AUT:Person/P:IdentityDocument/IDBD:IdentitityIssueDate, 'dd.MM.yyyy')"/>г.
                      </xsl:when>
                    </xsl:choose>
                    &#160; от &#160;
                    <xsl:value-of  select="AFIDOI:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Author/AUT:Person/P:IdentityDocument/IDBD:IdentityIssuer"/>
                  </td>
                </tr>
              </xsl:when>
              <xsl:otherwise>
                <tr>
                  <td colspan="2">
                    От&#160;
                    <xsl:value-of  select="AFIDOI:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Person/P:Names/NM:First/."/>
                    &#160;
                    <xsl:choose>
                      <xsl:when test="AFIDOI:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Person/P:Names/NM:Middle/.">
                        <xsl:value-of  select="AFIDOI:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Person/P:Names/NM:Middle/."/>&#160;
                      </xsl:when>
                    </xsl:choose>
                    &#160;
                    <xsl:value-of  select="AFIDOI:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Person/P:Names/NM:Last/."/>
                  </td>
                </tr>
                <tr>
                  <td colspan="2">
                    ЕГН/ЛНЧ:&#160;
                    <xsl:value-of  select="AFIDOI:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Person/P:Identifier/."/>
                  </td>
                </tr>
                <tr>
                  <td colspan="2">
                    <xsl:choose>
                      <xsl:when test="AFIDOI:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Person/P:IdentityDocument/IDBD:IdentityDocumentType = '0006-000087'">
                        Лична карта
                      </xsl:when>
                    </xsl:choose>
                    <xsl:choose>
                      <xsl:when test="AFIDOI:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Person/P:IdentityDocument/IDBD:IdentityDocumentType = '0006-000088'">
                        Паспорт
                      </xsl:when>
                    </xsl:choose>
                    <xsl:choose>
                      <xsl:when test="AFIDOI:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Person/P:IdentityDocument/IDBD:IdentityDocumentType = '0006-000089'">
                        Дипломатически паспорт
                      </xsl:when>
                    </xsl:choose>
                    <xsl:choose>
                      <xsl:when test="AFIDOI:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Person/P:IdentityDocument/IDBD:IdentityDocumentType = '0006-000090'">
                        Служебен паспорт
                      </xsl:when>
                    </xsl:choose>
                    <xsl:choose>
                      <xsl:when test="AFIDOI:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Person/P:IdentityDocument/IDBD:IdentityDocumentType = '0006-000091'">
                        Моряшки паспорт
                      </xsl:when>
                    </xsl:choose>
                    <xsl:choose>
                      <xsl:when test="AFIDOI:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Person/P:IdentityDocument/IDBD:IdentityDocumentType = '0006-000092'">
                        Военна карта за самоличност
                      </xsl:when>
                    </xsl:choose>
                    <xsl:choose>
                      <xsl:when test="AFIDOI:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Person/P:IdentityDocument/IDBD:IdentityDocumentType = '0006-000093'">
                        Свидетелство за управление на моторно превозно средство
                      </xsl:when>
                    </xsl:choose>
                    <xsl:choose>
                      <xsl:when test="AFIDOI:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Person/P:IdentityDocument/IDBD:IdentityDocumentType = '0006-000094'">
                        Временен паспорт
                      </xsl:when>
                    </xsl:choose>
                    <xsl:choose>
                      <xsl:when test="AFIDOI:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Person/P:IdentityDocument/IDBD:IdentityDocumentType = '0006-000095'">
                        Служебен открит лист за преминаване на границата
                      </xsl:when>
                    </xsl:choose>
                    <xsl:choose>
                      <xsl:when test="AFIDOI:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Person/P:IdentityDocument/IDBD:IdentityDocumentType = '0006-000097'">
                        Карта на бежанец
                      </xsl:when>
                    </xsl:choose>
                    <xsl:choose>
                      <xsl:when test="AFIDOI:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Person/P:IdentityDocument/IDBD:IdentityDocumentType = '0006-000099'">
                        Карта на чужденец с хуманитарен статут
                      </xsl:when>
                    </xsl:choose>
                    <xsl:choose>
                      <xsl:when test="AFIDOI:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Person/P:IdentityDocument/IDBD:IdentityDocumentType = '0006-000098'">
                        Карта на чужденец, получил убежище
                      </xsl:when>
                    </xsl:choose>
                    <xsl:choose>
                      <xsl:when test="AFIDOI:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Person/P:IdentityDocument/IDBD:IdentityDocumentType = '0006-000121'">
                        Разрешение за пребиваване
                      </xsl:when>
                    </xsl:choose>
                    <xsl:choose>
                      <xsl:when test="AFIDOI:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Person/P:IdentityDocument/IDBD:IdentityDocumentType = '0006-000122'">
                        Удостоверение за пребиваване на гражданин на ЕС
                      </xsl:when>
                    </xsl:choose>
                    &#160;№:&#160;<xsl:value-of  select="AFIDOI:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Person/P:IdentityDocument/IDBD:IdentityNumber"/>,
                    <br/>
                    изд. на:&#160;
                    <xsl:choose>
                      <xsl:when test="AFIDOI:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Person/P:IdentityDocument/IDBD:IdentitityIssueDate">
                        <xsl:value-of select="xslExtension:FormatDate(AFIDOI:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Person/P:IdentityDocument/IDBD:IdentitityIssueDate, 'dd.MM.yyyy')"/>г.
                      </xsl:when>
                    </xsl:choose>
                    &#160;от &#160;
                    <xsl:value-of  select="AFIDOI:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Person/P:IdentityDocument/IDBD:IdentityIssuer"/>
                  </td>
                </tr>
              </xsl:otherwise>
            </xsl:choose>
            <tr>
              <td colspan="2">
                Електронен адрес&#160;<xsl:value-of  select="AFIDOI:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:EmailAddress"/>
              </td>
            </tr>
            <tr>
              <td colspan ="2">
                Адрес за кореспонденция на получателя:&#160;
                <xsl:choose>
                  <xsl:when test="AFIDOI:ApplicationForIssuingDocumentsOfIncurredAndOtherAccidentsAndProvideInformationOnTheActivityCarriedOutData/AFIDOID:CorespondingAddress/CADR:DistrictName">
                    Обл.&#160;<xsl:value-of  select="AFIDOI:ApplicationForIssuingDocumentsOfIncurredAndOtherAccidentsAndProvideInformationOnTheActivityCarriedOutData/AFIDOID:CorespondingAddress/CADR:DistrictName"/>&#160;
                  </xsl:when>
                </xsl:choose>
                <xsl:choose>
                  <xsl:when test="AFIDOI:ApplicationForIssuingDocumentsOfIncurredAndOtherAccidentsAndProvideInformationOnTheActivityCarriedOutData/AFIDOID:CorespondingAddress/CADR:MunicipalityName">
                    Общ.&#160;<xsl:value-of  select="AFIDOI:ApplicationForIssuingDocumentsOfIncurredAndOtherAccidentsAndProvideInformationOnTheActivityCarriedOutData/AFIDOID:CorespondingAddress/CADR:MunicipalityName"/>&#160;
                  </xsl:when>
                </xsl:choose>
                <xsl:choose>
                  <xsl:when test="AFIDOI:ApplicationForIssuingDocumentsOfIncurredAndOtherAccidentsAndProvideInformationOnTheActivityCarriedOutData/AFIDOID:CorespondingAddress/CADR:SettlementName">
                    гр./с.&#160;<xsl:value-of  select="AFIDOI:ApplicationForIssuingDocumentsOfIncurredAndOtherAccidentsAndProvideInformationOnTheActivityCarriedOutData/AFIDOID:CorespondingAddress/CADR:SettlementName"/>&#160;
                  </xsl:when>
                </xsl:choose>
                <xsl:choose>
                  <xsl:when test="AFIDOI:ApplicationForIssuingDocumentsOfIncurredAndOtherAccidentsAndProvideInformationOnTheActivityCarriedOutData/AFIDOID:CorespondingAddress/CADR:AreaName">
                    р-н.&#160;<xsl:value-of  select="AFIDOI:ApplicationForIssuingDocumentsOfIncurredAndOtherAccidentsAndProvideInformationOnTheActivityCarriedOutData/AFIDOID:CorespondingAddress/CADR:AreaName"/>&#160;
                  </xsl:when>
                </xsl:choose>
                <xsl:choose>
                  <xsl:when test="AFIDOI:ApplicationForIssuingDocumentsOfIncurredAndOtherAccidentsAndProvideInformationOnTheActivityCarriedOutData/AFIDOID:CorespondingAddress/CADR:PostCode">
                    п.к.&#160;<xsl:value-of  select="AFIDOI:ApplicationForIssuingDocumentsOfIncurredAndOtherAccidentsAndProvideInformationOnTheActivityCarriedOutData/AFIDOID:CorespondingAddress/CADR:PostCode"/>&#160;
                  </xsl:when>
                </xsl:choose>
                <xsl:choose>
                  <xsl:when test="AFIDOI:ApplicationForIssuingDocumentsOfIncurredAndOtherAccidentsAndProvideInformationOnTheActivityCarriedOutData/AFIDOID:CorespondingAddress/CADR:HousingEstate">
                    ж.к.&#160;<xsl:value-of  select="AFIDOI:ApplicationForIssuingDocumentsOfIncurredAndOtherAccidentsAndProvideInformationOnTheActivityCarriedOutData/AFIDOID:CorespondingAddress/CADR:HousingEstate"/>&#160;
                  </xsl:when>
                </xsl:choose>
                <xsl:choose>
                  <xsl:when test="AFIDOI:ApplicationForIssuingDocumentsOfIncurredAndOtherAccidentsAndProvideInformationOnTheActivityCarriedOutData/AFIDOID:CorespondingAddress/CADR:Street">
                    бул./ул.&#160;<xsl:value-of  select="AFIDOI:ApplicationForIssuingDocumentsOfIncurredAndOtherAccidentsAndProvideInformationOnTheActivityCarriedOutData/AFIDOID:CorespondingAddress/CADR:Street"/>&#160;
                  </xsl:when>
                </xsl:choose>
                <xsl:choose>
                  <xsl:when test="AFIDOI:ApplicationForIssuingDocumentsOfIncurredAndOtherAccidentsAndProvideInformationOnTheActivityCarriedOutData/AFIDOID:CorespondingAddress/CADR:StreetNumber">
                    №&#160;<xsl:value-of  select="AFIDOI:ApplicationForIssuingDocumentsOfIncurredAndOtherAccidentsAndProvideInformationOnTheActivityCarriedOutData/AFIDOID:CorespondingAddress/CADR:StreetNumber"/>&#160;
                  </xsl:when>
                </xsl:choose>
                <xsl:choose>
                  <xsl:when test="AFIDOI:ApplicationForIssuingDocumentsOfIncurredAndOtherAccidentsAndProvideInformationOnTheActivityCarriedOutData/AFIDOID:CorespondingAddress/CADR:Block">
                    бл.&#160;<xsl:value-of  select="AFIDOI:ApplicationForIssuingDocumentsOfIncurredAndOtherAccidentsAndProvideInformationOnTheActivityCarriedOutData/AFIDOID:CorespondingAddress/CADR:Block"/>&#160;
                  </xsl:when>
                </xsl:choose>
                <xsl:choose>
                  <xsl:when test="AFIDOI:ApplicationForIssuingDocumentsOfIncurredAndOtherAccidentsAndProvideInformationOnTheActivityCarriedOutData/AFIDOID:CorespondingAddress/CADR:Entrance">
                    вх.<xsl:value-of  select="AFIDOI:ApplicationForIssuingDocumentsOfIncurredAndOtherAccidentsAndProvideInformationOnTheActivityCarriedOutData/AFIDOID:CorespondingAddress/CADR:Entrance"/>&#160;
                  </xsl:when>
                </xsl:choose>
                <xsl:choose>
                  <xsl:when test="AFIDOI:ApplicationForIssuingDocumentsOfIncurredAndOtherAccidentsAndProvideInformationOnTheActivityCarriedOutData/AFIDOID:CorespondingAddress/CADR:Floor">
                    ет.<xsl:value-of  select="AFIDOI:ApplicationForIssuingDocumentsOfIncurredAndOtherAccidentsAndProvideInformationOnTheActivityCarriedOutData/AFIDOID:CorespondingAddress/CADR:Floor"/>&#160;
                  </xsl:when>
                </xsl:choose>
                <xsl:choose>
                  <xsl:when test="AFIDOI:ApplicationForIssuingDocumentsOfIncurredAndOtherAccidentsAndProvideInformationOnTheActivityCarriedOutData/AFIDOID:CorespondingAddress/CADR:Apartment">
                    ап.&#160;<xsl:value-of  select="AFIDOI:ApplicationForIssuingDocumentsOfIncurredAndOtherAccidentsAndProvideInformationOnTheActivityCarriedOutData/AFIDOID:CorespondingAddress/CADR:Apartment"/>&#160;
                  </xsl:when>
                </xsl:choose>
              </td>
            </tr>
            <tr>
              <td colspan="2">
                Моля да ми бъде издаден &#160;
                <xsl:choose>
                  <xsl:when test="AFIDOI:ApplicationForIssuingDocumentsOfIncurredAndOtherAccidentsAndProvideInformationOnTheActivityCarriedOutData/AFIDOID:DocumentCertifyingTheAccidentOccurredOrOtherInformation ">
                    <xsl:value-of  select="AFIDOI:ApplicationForIssuingDocumentsOfIncurredAndOtherAccidentsAndProvideInformationOnTheActivityCarriedOutData/AFIDOID:DocumentCertifyingTheAccidentOccurredOrOtherInformation"/>
                  </xsl:when>
                </xsl:choose>
              </td>
            </tr>
            <xsl:choose>
              <xsl:when test="AFIDOI:ApplicationForIssuingDocumentsOfIncurredAndOtherAccidentsAndProvideInformationOnTheActivityCarriedOutData/AFIDOID:IncludeInformation107 = 'true' ">
                <tr>
                  <td colspan="2">
                    Желая да бъде включена информация за причинените от произшествието вреди във връзка с чл. 107, ал. 1 от Кодекса за застраховането.
                  </td>
                </tr>
              </xsl:when>
            </xsl:choose>
            <xsl:choose>
              <xsl:when test="AFIDOI:ApplicationForIssuingDocumentsOfIncurredAndOtherAccidentsAndProvideInformationOnTheActivityCarriedOutData/AFIDOID:IncludeInformation133 = 'true' ">
                <tr>
                  <td colspan="2">
                    Желая да бъде включена информация за причинените от произшествието вреди във връзка с чл. 133, ал. 1 от Наказателно-процесуалния кодекс.
                  </td>
                </tr>
              </xsl:when>
            </xsl:choose>
            <tr>
              <td colspan="2">
                Удостоверението да послужи: &#160;
                <xsl:choose>
                  <xsl:when test="AFIDOI:ApplicationForIssuingDocumentsOfIncurredAndOtherAccidentsAndProvideInformationOnTheActivityCarriedOutData/AFIDOID:DocumentMustServeTo/DMST:InRepublicOfBulgariaDocumentMustServeTo ">
                    <xsl:value-of  select="AFIDOI:ApplicationForIssuingDocumentsOfIncurredAndOtherAccidentsAndProvideInformationOnTheActivityCarriedOutData/AFIDOID:DocumentMustServeTo/DMST:InRepublicOfBulgariaDocumentMustServeTo"/>
                  </xsl:when>
                  <xsl:when test="AFIDOI:ApplicationForIssuingDocumentsOfIncurredAndOtherAccidentsAndProvideInformationOnTheActivityCarriedOutData/AFIDOID:DocumentMustServeTo/DMST:AbroadDocumentMustServeTo ">
                    <xsl:value-of  select="AFIDOI:ApplicationForIssuingDocumentsOfIncurredAndOtherAccidentsAndProvideInformationOnTheActivityCarriedOutData/AFIDOID:DocumentMustServeTo/DMST:AbroadDocumentMustServeTo"/>
                  </xsl:when>
                </xsl:choose>
              </td>
            </tr>
            <tr>
              <td colspan="2">
                Желая да получа искания документ:
                <xsl:choose>
                  <xsl:when test="AFIDOI:ServiceApplicantReceiptData/AA:ServiceResultReceiptMethod = '0006-000076' ">
                    &#160;като електронен документ
                  </xsl:when>
                </xsl:choose>
                <xsl:choose>
                  <xsl:when test="AFIDOI:ServiceApplicantReceiptData/AA:ServiceResultReceiptMethod = '0006-000077' ">
                    &#160;на мястото на заявяване
                  </xsl:when>
                </xsl:choose>
                <xsl:choose>
                  <xsl:when test="AFIDOI:ServiceApplicantReceiptData/AA:ServiceResultReceiptMethod = '0006-000079' ">
                    &#160;на посочения в искането адрес, чрез лицензиран пощенски оператор, като декларирам, че пощенските разходи са за моя сметка
                  </xsl:when>
                </xsl:choose>
                <xsl:choose>
                  <xsl:when test="AFIDOI:ServiceApplicantReceiptData/AA:ServiceResultReceiptMethod = '0006-000080' ">
                    &#160;чрез лицензиран пощенски оператор на адрес:&#160;
                    <xsl:choose>
                      <xsl:when test="AFIDOI:ServiceApplicantReceiptData/AA:ApplicantAdress/AA:DistrictName">
                        Обл.&#160;<xsl:value-of  select="AFIDOI:ServiceApplicantReceiptData/AA:ApplicantAdress/AA:DistrictName"/>&#160;
                      </xsl:when>
                    </xsl:choose>
                    <xsl:choose>
                      <xsl:when test="AFIDOI:ServiceApplicantReceiptData/AA:ApplicantAdress/AA:MunicipalityName">
                        Общ.&#160;<xsl:value-of  select="AFIDOI:ServiceApplicantReceiptData/AA:ApplicantAdress/AA:MunicipalityName"/>&#160;
                      </xsl:when>
                    </xsl:choose>
                    <xsl:choose>
                      <xsl:when test="AFIDOI:ServiceApplicantReceiptData/AA:ApplicantAdress/AA:SettlementName">
                        гр./с.&#160;<xsl:value-of  select="AFIDOI:ServiceApplicantReceiptData/AA:ApplicantAdress/AA:SettlementName"/>&#160;
                      </xsl:when>
                    </xsl:choose>
                    <xsl:choose>
                      <xsl:when test="AFIDOI:ServiceApplicantReceiptData/AA:ApplicantAdress/AA:AreaName">
                        р-н.&#160;<xsl:value-of  select="AFIDOI:ServiceApplicantReceiptData/AA:ApplicantAdress/AA:AreaName"/>&#160;
                      </xsl:when>
                    </xsl:choose>
                    <xsl:choose>
                      <xsl:when test="AFIDOI:ServiceApplicantReceiptData/AA:ApplicantAdress/AA:PostCode">
                        п.к.&#160;<xsl:value-of  select="AFIDOI:ServiceApplicantReceiptData/AA:ApplicantAdress/AA:PostCode"/>&#160;
                      </xsl:when>
                    </xsl:choose>
                    <xsl:choose>
                      <xsl:when test="AFIDOI:ServiceApplicantReceiptData/AA:ApplicantAdress/AA:HousingEstate">
                        ж.к.&#160;<xsl:value-of  select="AFIDOI:ServiceApplicantReceiptData/AA:ApplicantAdress/AA:HousingEstate"/>&#160;
                      </xsl:when>
                    </xsl:choose>
                    <xsl:choose>
                      <xsl:when test="AFIDOI:ServiceApplicantReceiptData/AA:ApplicantAdress/AA:Street">
                        бул./ул.&#160;<xsl:value-of  select="AFIDOI:ServiceApplicantReceiptData/AA:ApplicantAdress/AA:Street"/>&#160;
                      </xsl:when>
                    </xsl:choose>
                    <xsl:choose>
                      <xsl:when test="AFIDOI:ServiceApplicantReceiptData/AA:ApplicantAdress/AA:StreetNumber">
                        №&#160;<xsl:value-of  select="AFIDOI:ServiceApplicantReceiptData/AA:ApplicantAdress/AA:StreetNumber"/>&#160;
                      </xsl:when>
                    </xsl:choose>
                    <xsl:choose>
                      <xsl:when test="AFIDOI:ServiceApplicantReceiptData/AA:ApplicantAdress/AA:Block">
                        бл.&#160;<xsl:value-of  select="AFIDOI:ServiceApplicantReceiptData/AA:ApplicantAdress/AA:Block"/>&#160;
                      </xsl:when>
                    </xsl:choose>
                    <xsl:choose>
                      <xsl:when test="AFIDOI:ServiceApplicantReceiptData/AA:ApplicantAdress/AA:Entrance">
                        вх.<xsl:value-of  select="AFIDOI:ServiceApplicantReceiptData/AA:ApplicantAdress/AA:Entrance"/>&#160;
                      </xsl:when>
                    </xsl:choose>
                    <xsl:choose>
                      <xsl:when test="AFIDOI:ServiceApplicantReceiptData/AA:ApplicantAdress/AA:Floor">
                        ет.<xsl:value-of  select="AFIDOI:ServiceApplicantReceiptData/AA:ApplicantAdress/AA:Floor"/>&#160;
                      </xsl:when>
                    </xsl:choose>
                    <xsl:choose>
                      <xsl:when test="AFIDOI:ServiceApplicantReceiptData/AA:ApplicantAdress/AA:Apartment">
                        ап.&#160;<xsl:value-of  select="AFIDOI:ServiceApplicantReceiptData/AA:ApplicantAdress/AA:Apartment"/>&#160;
                      </xsl:when>
                    </xsl:choose>
                    <xsl:value-of  select="AFIDOI:ServiceApplicantReceiptData/AA:ApplicantAdress/AA:AddressDescription"/>
                  </xsl:when>
                </xsl:choose>
              </td>

            </tr>
            <xsl:choose>
              <xsl:when test = "AFIDOI:Declarations">
                <xsl:for-each select="AFIDOI:Declarations/AFIDOI:Declaration">
                  <xsl:choose>
                    <xsl:when test="DECL:IsDeclarationFilled = 'true'">
                      <tr>
                        <td colspan="2">
                          <xsl:value-of  select="DECL:DeclarationName" disable-output-escaping="yes"/>
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
              <xsl:when test = "AFIDOI:AttachedDocuments">
                <tr>
                  <td colspan="2">
                    Приложени документи:
                  </td>
                </tr>
                <tr>
                  <td colspan="2">
                    <ol>
                      <xsl:for-each select="AFIDOI:AttachedDocuments/AFIDOI:AttachedDocument">
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
                Дата:
                <xsl:choose>
                  <xsl:when test="AFIDOI:ElectronicAdministrativeServiceFooter/EASF:ApplicationSigningTime">
                    <xsl:value-of select="xslExtension:FormatDate(AFIDOI:ElectronicAdministrativeServiceFooter/EASF:ApplicationSigningTime, 'dd.MM.yyyy')"/>г.
                  </xsl:when>
                </xsl:choose>
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