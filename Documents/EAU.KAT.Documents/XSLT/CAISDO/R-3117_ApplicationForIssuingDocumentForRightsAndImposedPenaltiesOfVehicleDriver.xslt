<xsl:stylesheet version="1.0" xmlns:AIDRIPVD="http://ereg.egov.bg/segment/R-3117"
                xmlns:EASH="http://ereg.egov.bg/segment/0009-000152"
				        xmlns:ESA="http://ereg.egov.bg/segment/0009-000016"
				        xmlns:REC="http://ereg.egov.bg/segment/0009-000015"
				        xmlns:P="http://ereg.egov.bg/segment/0009-000008"
				        xmlns:NM="http://ereg.egov.bg/segment/0009-000005"
				        xmlns:ID="http://ereg.egov.bg/segment/0009-000006"
				        xmlns:IDBD="http://ereg.egov.bg/segment/0009-000099"
				        xmlns:PA="http://ereg.egov.bg/segment/0009-000094"
				        xmlns:AIDRIPVDD="http://ereg.egov.bg/segment/R-3118"
				        xmlns:PI="http://ereg.egov.bg/segment/R-3015"
				        xmlns:AUT="http://ereg.egov.bg/segment/0009-000012"
				        xmlns:DBIF="http://ereg.egov.bg/segment/R-3041"
				        xmlns:IBDIP="http://ereg.egov.bg/segment/R-3033"
				        xmlns:OICIBID="http://ereg.egov.bg/value/R-3034"
				        xmlns:DMST="http://ereg.egov.bg/segment/R-3040"
				        xmlns:SARD="http://ereg.egov.bg/segment/0009-000141"
				        xmlns:EASF="http://ereg.egov.bg/segment/0009-000153"
                xmlns:DECL="http://ereg.egov.bg/segment//R-3136"
                xmlns:ds="http://www.w3.org/2000/09/xmldsig#"
                xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
                xmlns:ADD="http://ereg.egov.bg/segment/0009-000139"
                xmlns:xslExtension="urn:XSLExtension"
                xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
				
xmlns:ms="urn:schemas-microsoft-com:xslt" xsi:type="xsl:transform" >
  <xsl:include href="./KATBaseTemplates.xslt"/>

  <xsl:output omit-xml-declaration="yes" method="html"/>
  <xsl:template match="AIDRIPVD:ApplicationForIssuingDocumentForRightsAndImposedPenaltiesOfVehicleDriver">
    <xsl:text disable-output-escaping='yes'>&lt;!DOCTYPE html&gt;</xsl:text>
    <html>
      <xsl:call-template name="Head"/>
      <body>
        <table align="center" cellpadding="5" width= "700px">
          <thead>
            <tr>
              <th colspan ="2">
                <h2>
                  <xsl:value-of select="AIDRIPVD:ElectronicAdministrativeServiceHeader/EASH:DocumentTypeName" />
                </h2>
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
              <td colspan ="2">
                &#160;
              </td>
            </tr>
            <tr>
              <td colspan ="2">
                От&#160;
                <xsl:choose>
                  <xsl:when test="AIDRIPVD:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Author/AUT:AuthorQualityType != 'R-1001'">
                    <xsl:value-of  select="AIDRIPVD:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Author/AUT:Person/P:Names/NM:First/."/>
                    &#160;
                    <xsl:choose>
                      <xsl:when test="AIDRIPVD:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Author/AUT:Person/P:Names/NM:Middle/.">
                        <xsl:value-of  select="AIDRIPVD:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Author/AUT:Person/P:Names/NM:Middle/."/>&#160;
                      </xsl:when>
                    </xsl:choose>
                    &#160;
                    <xsl:value-of  select="AIDRIPVD:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Author/AUT:Person/P:Names/NM:Last/."/>,
                    &#160;
                  </xsl:when>
                  <xsl:otherwise>
                    <xsl:value-of  select="AIDRIPVD:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Person/P:Names/NM:First/."/>
                    &#160;
                    <xsl:choose>
                      <xsl:when test="AIDRIPVD:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Person/P:Names/NM:Middle/.">
                        <xsl:value-of  select="AIDRIPVD:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Person/P:Names/NM:Middle/."/>&#160;
                      </xsl:when>
                    </xsl:choose>
                    <xsl:value-of  select="AIDRIPVD:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Person/P:Names/NM:Last/."/>
                  </xsl:otherwise>
                </xsl:choose>
                &#160;
                ЕГН/ЛНЧ
                &#160;
                <xsl:choose>
                  <xsl:when test="AIDRIPVD:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Author/AUT:AuthorQualityType != 'R-1001'">
                    <xsl:value-of  select="AIDRIPVD:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Author/AUT:Person/P:Identifier/."/>
                  </xsl:when>
                  <xsl:otherwise>
                    <xsl:value-of  select="AIDRIPVD:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Person/P:Identifier/."/>
                  </xsl:otherwise>
                </xsl:choose>
              </td>
            </tr>
            <xsl:choose>
              <xsl:when test="AIDRIPVD:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Author/AUT:AuthorQualityType != 'R-1001'">
                <tr>
                  <td colspan="2">
                    <xsl:choose>
                      <xsl:when test="AIDRIPVD:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Author/AUT:Person/P:IdentityDocument/IDBD:IdentityDocumentType = '0006-000087'">
                        Лична карта
                      </xsl:when>
                    </xsl:choose>
                    <xsl:choose>
                      <xsl:when test="AIDRIPVD:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Author/AUT:Person/P:IdentityDocument/IDBD:IdentityDocumentType = '0006-000088'">
                        Паспорт
                      </xsl:when>
                    </xsl:choose>
                    <xsl:choose>
                      <xsl:when test="AIDRIPVD:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Author/AUT:Person/P:IdentityDocument/IDBD:IdentityDocumentType = '0006-000089'">
                        Дипломатически паспорт
                      </xsl:when>
                    </xsl:choose>
                    <xsl:choose>
                      <xsl:when test="AIDRIPVD:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Author/AUT:Person/P:IdentityDocument/IDBD:IdentityDocumentType = '0006-000090'">
                        Служебен паспорт
                      </xsl:when>
                    </xsl:choose>
                    <xsl:choose>
                      <xsl:when test="AIDRIPVD:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Author/AUT:Person/P:IdentityDocument/IDBD:IdentityDocumentType = '0006-000091'">
                        Моряшки паспорт
                      </xsl:when>
                    </xsl:choose>
                    <xsl:choose>
                      <xsl:when test="AIDRIPVD:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Author/AUT:Person/P:IdentityDocument/IDBD:IdentityDocumentType = '0006-000092'">
                        Военна карта за самоличност
                      </xsl:when>
                    </xsl:choose>
                    <xsl:choose>
                      <xsl:when test="AIDRIPVD:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Author/AUT:Person/P:IdentityDocument/IDBD:IdentityDocumentType = '0006-000093'">
                        Свидетелство за управление на моторно превозно средство
                      </xsl:when>
                    </xsl:choose>
                    <xsl:choose>
                      <xsl:when test="AIDRIPVD:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Author/AUT:Person/P:IdentityDocument/IDBD:IdentityDocumentType = '0006-000094'">
                        Временен паспорт
                      </xsl:when>
                    </xsl:choose>
                    <xsl:choose>
                      <xsl:when test="AIDRIPVD:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Author/AUT:Person/P:IdentityDocument/IDBD:IdentityDocumentType = '0006-000095'">
                        Служебен открит лист за преминаване на границата
                      </xsl:when>
                    </xsl:choose>
                    <xsl:choose>
                      <xsl:when test="AIDRIPVD:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Author/AUT:Person/P:IdentityDocument/IDBD:IdentityDocumentType = '0006-000097'">
                        Карта на бежанец
                      </xsl:when>
                    </xsl:choose>
                    <xsl:choose>
                      <xsl:when test="AIDRIPVD:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Author/AUT:Person/P:IdentityDocument/IDBD:IdentityDocumentType = '0006-000099'">
                        Карта на чужденец с хуманитарен статут
                      </xsl:when>
                    </xsl:choose>
                    <xsl:choose>
                      <xsl:when test="AIDRIPVD:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Author/AUT:Person/P:IdentityDocument/IDBD:IdentityDocumentType = '0006-000098'">
                        Карта на чужденец, получил убежище
                      </xsl:when>
                    </xsl:choose>
                    <xsl:choose>
                      <xsl:when test="AIDRIPVD:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Author/AUT:Person/P:IdentityDocument/IDBD:IdentityDocumentType = '0006-000121'">
                        Разрешение за пребиваване
                      </xsl:when>
                    </xsl:choose>
                    <xsl:choose>
                      <xsl:when test="AIDRIPVD:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Author/AUT:Person/P:IdentityDocument/IDBD:IdentityDocumentType = '0006-000122'">
                        Удостоверение за пребиваване на гражданин на ЕС
                      </xsl:when>
                    </xsl:choose>

                    &#160;№&#160;<xsl:value-of  select="AIDRIPVD:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Person/P:IdentityDocument/IDBD:IdentityNumber"/><br/>
                    изд. на&#160;
                    <xsl:value-of  select="ms:format-date(AIDRIPVD:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Person/P:IdentityDocument/IDBD:IdentitityIssueDate , 'dd.MM.yyyy')"/>г.
                    &#160;от&#160;
                    <xsl:value-of  select="AIDRIPVD:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Person/P:IdentityDocument/IDBD:IdentityIssuer"/>
                  </td>
                </tr>
              </xsl:when>
              <xsl:otherwise>
                <tr>
                  <td colspan="2">
                    <xsl:choose>
                      <xsl:when test="AIDRIPVD:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Person/P:IdentityDocument/IDBD:IdentityDocumentType = '0006-000087'">
                        Лична карта
                      </xsl:when>
                    </xsl:choose>
                    <xsl:choose>
                      <xsl:when test="AIDRIPVD:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Person/P:IdentityDocument/IDBD:IdentityDocumentType = '0006-000088'">
                        Паспорт
                      </xsl:when>
                    </xsl:choose>
                    <xsl:choose>
                      <xsl:when test="AIDRIPVD:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Person/P:IdentityDocument/IDBD:IdentityDocumentType = '0006-000089'">
                        Дипломатически паспорт
                      </xsl:when>
                    </xsl:choose>
                    <xsl:choose>
                      <xsl:when test="AIDRIPVD:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Person/P:IdentityDocument/IDBD:IdentityDocumentType = '0006-000090'">
                        Служебен паспорт
                      </xsl:when>
                    </xsl:choose>
                    <xsl:choose>
                      <xsl:when test="AIDRIPVD:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Person/P:IdentityDocument/IDBD:IdentityDocumentType = '0006-000091'">
                        Моряшки паспорт
                      </xsl:when>
                    </xsl:choose>
                    <xsl:choose>
                      <xsl:when test="AIDRIPVD:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Person/P:IdentityDocument/IDBD:IdentityDocumentType = '0006-000092'">
                        Военна карта за самоличност
                      </xsl:when>
                    </xsl:choose>
                    <xsl:choose>
                      <xsl:when test="AIDRIPVD:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Person/P:IdentityDocument/IDBD:IdentityDocumentType = '0006-000093'">
                        Свидетелство за управление на моторно превозно средство
                      </xsl:when>
                    </xsl:choose>
                    <xsl:choose>
                      <xsl:when test="AIDRIPVD:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Person/P:IdentityDocument/IDBD:IdentityDocumentType = '0006-000094'">
                        Временен паспорт
                      </xsl:when>
                    </xsl:choose>
                    <xsl:choose>
                      <xsl:when test="AIDRIPVD:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Person/P:IdentityDocument/IDBD:IdentityDocumentType = '0006-000095'">
                        Служебен открит лист за преминаване на границата
                      </xsl:when>
                    </xsl:choose>
                    <xsl:choose>
                      <xsl:when test="AIDRIPVD:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Person/P:IdentityDocument/IDBD:IdentityDocumentType = '0006-000097'">
                        Карта на бежанец
                      </xsl:when>
                    </xsl:choose>
                    <xsl:choose>
                      <xsl:when test="AIDRIPVD:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Person/P:IdentityDocument/IDBD:IdentityDocumentType = '0006-000099'">
                        Карта на чужденец с хуманитарен статут
                      </xsl:when>
                    </xsl:choose>
                    <xsl:choose>
                      <xsl:when test="AIDRIPVD:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Person/P:IdentityDocument/IDBD:IdentityDocumentType = '0006-000098'">
                        Карта на чужденец, получил убежище
                      </xsl:when>
                    </xsl:choose>
                    <xsl:choose>
                      <xsl:when test="AIDRIPVD:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Person/P:IdentityDocument/IDBD:IdentityDocumentType = '0006-000121'">
                        Разрешение за пребиваване
                      </xsl:when>
                    </xsl:choose>
                    <xsl:choose>
                      <xsl:when test="AIDRIPVD:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Person/P:IdentityDocument/IDBD:IdentityDocumentType = '0006-000122'">
                        Удостоверение за пребиваване на гражданин на ЕС
                      </xsl:when>
                    </xsl:choose>
                    &#160;№&#160;<xsl:value-of  select="AIDRIPVD:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Person/P:IdentityDocument/IDBD:IdentityNumber"/><br/>
                    изд. на&#160;
                    <xsl:value-of  select="ms:format-date(AIDRIPVD:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Person/P:IdentityDocument/IDBD:IdentitityIssueDate , 'dd.MM.yyyy')"/>г.
                    &#160;от&#160;
                    <xsl:value-of  select="AIDRIPVD:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Person/P:IdentityDocument/IDBD:IdentityIssuer"/>
                  </td>
                </tr>
              </xsl:otherwise>
            </xsl:choose>

            <tr>
              <td colspan ="2">
                Постоянен адрес:&#160;
                <xsl:choose>
                  <xsl:when test="AIDRIPVD:ApplicationForIssuingDocumentForRightsAndImposedPenaltiesOfVehicleDriverData/AIDRIPVDD:PermanentAddress/PA:DistrictGRAOName">
                    Обл.&#160;<xsl:value-of  select="AIDRIPVD:ApplicationForIssuingDocumentForRightsAndImposedPenaltiesOfVehicleDriverData/AIDRIPVDD:PermanentAddress/PA:DistrictGRAOName"/>&#160;
                  </xsl:when>
                </xsl:choose>
                <xsl:choose>
                  <xsl:when test="AIDRIPVD:ApplicationForIssuingDocumentForRightsAndImposedPenaltiesOfVehicleDriverData/AIDRIPVDD:PermanentAddress/PA:MunicipalityGRAOName">
                    Общ.&#160;<xsl:value-of  select="AIDRIPVD:ApplicationForIssuingDocumentForRightsAndImposedPenaltiesOfVehicleDriverData/AIDRIPVDD:PermanentAddress/PA:MunicipalityGRAOName"/>&#160;
                  </xsl:when>
                </xsl:choose>
                <xsl:choose>
                  <xsl:when test="AIDRIPVD:ApplicationForIssuingDocumentForRightsAndImposedPenaltiesOfVehicleDriverData/AIDRIPVDD:PermanentAddress/PA:DistrictGRAOName">
                    &#160;<xsl:value-of  select="AIDRIPVD:ApplicationForIssuingDocumentForRightsAndImposedPenaltiesOfVehicleDriverData/AIDRIPVDD:PermanentAddress/PA:SettlementGRAOName"/>&#160;<br/>
                  </xsl:when>
                </xsl:choose>
                <xsl:choose>
                  <xsl:when test="AIDRIPVD:ApplicationForIssuingDocumentForRightsAndImposedPenaltiesOfVehicleDriverData/AIDRIPVDD:PermanentAddress/PA:StreetText">
                    ул.&#160;<xsl:value-of  select="AIDRIPVD:ApplicationForIssuingDocumentForRightsAndImposedPenaltiesOfVehicleDriverData/AIDRIPVDD:PermanentAddress/PA:StreetText"/>&#160;
                  </xsl:when>
                </xsl:choose>
                <xsl:choose>
                  <xsl:when test="AIDRIPVD:ApplicationForIssuingDocumentForRightsAndImposedPenaltiesOfVehicleDriverData/AIDRIPVDD:PermanentAddress/PA:BuildingNumber">
                    №&#160;<xsl:value-of  select="AIDRIPVD:ApplicationForIssuingDocumentForRightsAndImposedPenaltiesOfVehicleDriverData/AIDRIPVDD:PermanentAddress/PA:BuildingNumber"/>&#160;
                  </xsl:when>
                </xsl:choose>
                <xsl:choose>
                  <xsl:when test="AIDRIPVD:ApplicationForIssuingDocumentForRightsAndImposedPenaltiesOfVehicleDriverData/AIDRIPVDD:PermanentAddress/PA:Entrance">
                    вх.&#160;<xsl:value-of  select="AIDRIPVD:ApplicationForIssuingDocumentForRightsAndImposedPenaltiesOfVehicleDriverData/AIDRIPVDD:PermanentAddress/PA:Entrance"/>&#160;
                  </xsl:when>
                </xsl:choose>
                <xsl:choose>
                  <xsl:when test="AIDRIPVD:ApplicationForIssuingDocumentForRightsAndImposedPenaltiesOfVehicleDriverData/AIDRIPVDD:PermanentAddress/PA:Floor">
                    ет.&#160;<xsl:value-of  select="AIDRIPVD:ApplicationForIssuingDocumentForRightsAndImposedPenaltiesOfVehicleDriverData/AIDRIPVDD:PermanentAddress/PA:Floor"/>&#160;
                  </xsl:when>
                </xsl:choose>
                <xsl:choose>
                  <xsl:when test="AIDRIPVD:ApplicationForIssuingDocumentForRightsAndImposedPenaltiesOfVehicleDriverData/AIDRIPVDD:PermanentAddress/PA:Apartment">
                    ап.&#160;<xsl:value-of  select="AIDRIPVD:ApplicationForIssuingDocumentForRightsAndImposedPenaltiesOfVehicleDriverData/AIDRIPVDD:PermanentAddress/PA:Apartment"/>&#160;
                  </xsl:when>
                </xsl:choose>
              </td>
            </tr>
            <tr>
              <td colspan ="2">
                Настоящ адрес:&#160;
                <xsl:choose>
                  <xsl:when test="AIDRIPVD:ApplicationForIssuingDocumentForRightsAndImposedPenaltiesOfVehicleDriverData/AIDRIPVDD:CurrentAddress/PA:DistrictGRAOName ">
                    Обл.&#160;<xsl:value-of  select="AIDRIPVD:ApplicationForIssuingDocumentForRightsAndImposedPenaltiesOfVehicleDriverData/AIDRIPVDD:CurrentAddress/PA:DistrictGRAOName"/>&#160;
                  </xsl:when>
                </xsl:choose>
                <xsl:choose>
                  <xsl:when test="AIDRIPVD:ApplicationForIssuingDocumentForRightsAndImposedPenaltiesOfVehicleDriverData/AIDRIPVDD:CurrentAddress/PA:MunicipalityGRAOName ">
                    Общ.&#160;<xsl:value-of  select="AIDRIPVD:ApplicationForIssuingDocumentForRightsAndImposedPenaltiesOfVehicleDriverData/AIDRIPVDD:CurrentAddress/PA:MunicipalityGRAOName"/>&#160;
                  </xsl:when>
                </xsl:choose>
                <xsl:choose>
                  <xsl:when test="AIDRIPVD:ApplicationForIssuingDocumentForRightsAndImposedPenaltiesOfVehicleDriverData/AIDRIPVDD:CurrentAddress/PA:DistrictGRAOName ">
                    &#160;<xsl:value-of  select="AIDRIPVD:ApplicationForIssuingDocumentForRightsAndImposedPenaltiesOfVehicleDriverData/AIDRIPVDD:CurrentAddress/PA:SettlementGRAOName"/>&#160;<br/>
                  </xsl:when>
                </xsl:choose>
                <xsl:choose>
                  <xsl:when test="AIDRIPVD:ApplicationForIssuingDocumentForRightsAndImposedPenaltiesOfVehicleDriverData/AIDRIPVDD:CurrentAddress/PA:StreetText ">
                    ул.&#160;<xsl:value-of  select="AIDRIPVD:ApplicationForIssuingDocumentForRightsAndImposedPenaltiesOfVehicleDriverData/AIDRIPVDD:CurrentAddress/PA:StreetText"/>&#160;
                  </xsl:when>
                </xsl:choose>
                <xsl:choose>
                  <xsl:when test="AIDRIPVD:ApplicationForIssuingDocumentForRightsAndImposedPenaltiesOfVehicleDriverData/AIDRIPVDD:CurrentAddress/PA:BuildingNumber ">
                    №&#160;<xsl:value-of  select="AIDRIPVD:ApplicationForIssuingDocumentForRightsAndImposedPenaltiesOfVehicleDriverData/AIDRIPVDD:CurrentAddress/PA:BuildingNumber"/>&#160;
                  </xsl:when>
                </xsl:choose>
                <xsl:choose>
                  <xsl:when test="AIDRIPVD:ApplicationForIssuingDocumentForRightsAndImposedPenaltiesOfVehicleDriverData/AIDRIPVDD:CurrentAddress/PA:Entrance ">
                    вх.&#160;<xsl:value-of  select="AIDRIPVD:ApplicationForIssuingDocumentForRightsAndImposedPenaltiesOfVehicleDriverData/AIDRIPVDD:CurrentAddress/PA:Entrance"/>&#160;
                  </xsl:when>
                </xsl:choose>
                <xsl:choose>
                  <xsl:when test="AIDRIPVD:ApplicationForIssuingDocumentForRightsAndImposedPenaltiesOfVehicleDriverData/AIDRIPVDD:CurrentAddress/PA:Floor ">
                    ет.&#160;<xsl:value-of  select="AIDRIPVD:ApplicationForIssuingDocumentForRightsAndImposedPenaltiesOfVehicleDriverData/AIDRIPVDD:CurrentAddress/PA:Floor"/>&#160;
                  </xsl:when>
                </xsl:choose>
                <xsl:choose>
                  <xsl:when test="AIDRIPVD:ApplicationForIssuingDocumentForRightsAndImposedPenaltiesOfVehicleDriverData/AIDRIPVDD:CurrentAddress/PA:Apartment ">
                    ап.&#160;<xsl:value-of  select="AIDRIPVD:ApplicationForIssuingDocumentForRightsAndImposedPenaltiesOfVehicleDriverData/AIDRIPVDD:CurrentAddress/PA:Apartment"/>&#160;
                  </xsl:when>
                </xsl:choose>
              </td>
            </tr>
            <tr>
              <td colspan ="2">
                <xsl:choose>
                  <xsl:when test="AIDRIPVD:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:EmailAddress">
                    Адрес на електронна поща:&#160;<xsl:value-of  select="AIDRIPVD:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:EmailAddress"/>
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
              <td colspan ="2">
                &#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;
                <xsl:choose>
                  <xsl:when test="AIDRIPVD:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Author/AUT:AuthorQualityType = 'R-1001'">
                    Моля да ми бъде издадено удостоверение за правата и наложените ми наказания като водач на МПС.
                  </xsl:when>
                  <xsl:when test="AIDRIPVD:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Author/AUT:AuthorQualityType = 'R-1002'">
                    Моля, в качеството на пълномощник, да ми бъде издадено удостоверение за правата и наложените наказания като водач на МПС на лицето:<br/>
                    <xsl:value-of  select="AIDRIPVD:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Person/P:Names/NM:First/."/>
                    &#160;
                    <xsl:value-of  select="AIDRIPVD:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Person/P:Names/NM:Middle/."/>
                    &#160;
                    <xsl:value-of  select="AIDRIPVD:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Person/P:Names/NM:Last/."/>
                    &#160;ЕГН/ЛНЧ&#160;
                    <xsl:value-of  select="AIDRIPVD:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Person/P:Identifier/."/>
                    <br/>
                    <xsl:choose>
                      <xsl:when test="AIDRIPVD:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Person/P:IdentityDocument/IDBD:IdentityDocumentType = '0006-000087'">
                        Лична карта
                      </xsl:when>
                    </xsl:choose>
                    <xsl:choose>
                      <xsl:when test="AIDRIPVD:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Person/P:IdentityDocument/IDBD:IdentityDocumentType = '0006-000088'">
                        Паспорт
                      </xsl:when>
                    </xsl:choose>
                    <xsl:choose>
                      <xsl:when test="AIDRIPVD:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Person/P:IdentityDocument/IDBD:IdentityDocumentType = '0006-000089'">
                        Дипломатически паспорт
                      </xsl:when>
                    </xsl:choose>
                    <xsl:choose>
                      <xsl:when test="AIDRIPVD:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Person/P:IdentityDocument/IDBD:IdentityDocumentType = '0006-000090'">
                        Служебен паспорт
                      </xsl:when>
                    </xsl:choose>
                    <xsl:choose>
                      <xsl:when test="AIDRIPVD:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Person/P:IdentityDocument/IDBD:IdentityDocumentType = '0006-000091'">
                        Моряшки паспорт
                      </xsl:when>
                    </xsl:choose>
                    <xsl:choose>
                      <xsl:when test="AIDRIPVD:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Person/P:IdentityDocument/IDBD:IdentityDocumentType = '0006-000092'">
                        Военна карта за самоличност
                      </xsl:when>
                    </xsl:choose>
                    <xsl:choose>
                      <xsl:when test="AIDRIPVD:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Person/P:IdentityDocument/IDBD:IdentityDocumentType = '0006-000093'">
                        Свидетелство за управление на моторно превозно средство
                      </xsl:when>
                    </xsl:choose>
                    <xsl:choose>
                      <xsl:when test="AIDRIPVD:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Person/P:IdentityDocument/IDBD:IdentityDocumentType = '0006-000094'">
                        Временен паспорт
                      </xsl:when>
                    </xsl:choose>
                    <xsl:choose>
                      <xsl:when test="AIDRIPVD:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Person/P:IdentityDocument/IDBD:IdentityDocumentType = '0006-000095'">
                        Служебен открит лист за преминаване на границата
                      </xsl:when>
                    </xsl:choose>
                    <xsl:choose>
                      <xsl:when test="AIDRIPVD:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Person/P:IdentityDocument/IDBD:IdentityDocumentType = '0006-000097'">
                        Карта на бежанец
                      </xsl:when>
                    </xsl:choose>
                    <xsl:choose>
                      <xsl:when test="AIDRIPVD:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Person/P:IdentityDocument/IDBD:IdentityDocumentType = '0006-000099'">
                        Карта на чужденец с хуманитарен статут
                      </xsl:when>
                    </xsl:choose>
                    <xsl:choose>
                      <xsl:when test="AIDRIPVD:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Person/P:IdentityDocument/IDBD:IdentityDocumentType = '0006-000098'">
                        Карта на чужденец, получил убежище
                      </xsl:when>
                    </xsl:choose>
                    <xsl:choose>
                      <xsl:when test="AIDRIPVD:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Person/P:IdentityDocument/IDBD:IdentityDocumentType = '0006-000121'">
                        Разрешение за пребиваване
                      </xsl:when>
                    </xsl:choose>
                    <xsl:choose>
                      <xsl:when test="AIDRIPVD:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Person/P:IdentityDocument/IDBD:IdentityDocumentType = '0006-000122'">
                        Удостоверение за пребиваване на гражданин на ЕС
                      </xsl:when>
                    </xsl:choose> &#160; №:<xsl:value-of  select="AIDRIPVD:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Person/P:IdentityDocument/IDBD:IdentityNumber"/>,
                    <br/>
                    изд. на:&#160;
                    <xsl:value-of  select="ms:format-date(AIDRIPVD:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Person/P:IdentityDocument/IDBD:IdentitityIssueDate , 'dd.MM.yyyy')"/>г.,
                    &#160;от &#160;
                    <xsl:value-of  select="AIDRIPVD:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Person/P:IdentityDocument/IDBD:IdentityIssuer"/>

                  </xsl:when>
                </xsl:choose>
              </td>

            </tr>
            <tr>
              <td colspan = "2">
                Същото да послужи:&#160;
                <xsl:choose>
                  <xsl:when test="AIDRIPVD:ApplicationForIssuingDocumentForRightsAndImposedPenaltiesOfVehicleDriverData/AIDRIPVDD:ANDCertificateReason = '2001' ">
                    за Служебна бележка
                  </xsl:when>
                </xsl:choose>
                <xsl:choose>
                  <xsl:when test="AIDRIPVD:ApplicationForIssuingDocumentForRightsAndImposedPenaltiesOfVehicleDriverData/AIDRIPVDD:ANDCertificateReason = '2002' ">
                    за Постъпване на работа
                  </xsl:when>
                </xsl:choose>
                <xsl:choose>
                  <xsl:when test="AIDRIPVD:ApplicationForIssuingDocumentForRightsAndImposedPenaltiesOfVehicleDriverData/AIDRIPVDD:ANDCertificateReason = '2003' ">
                    пред	Застрахователя
                  </xsl:when>
                </xsl:choose>
                <xsl:choose>
                  <xsl:when test="AIDRIPVD:ApplicationForIssuingDocumentForRightsAndImposedPenaltiesOfVehicleDriverData/AIDRIPVDD:ANDCertificateReason = '2004' ">
                    пред	Медицинските органи
                  </xsl:when>
                </xsl:choose>
                <xsl:choose>
                  <xsl:when test="AIDRIPVD:ApplicationForIssuingDocumentForRightsAndImposedPenaltiesOfVehicleDriverData/AIDRIPVDD:ANDCertificateReason = '2005' ">
                    пред	Съдебните власти
                  </xsl:when>
                </xsl:choose>
                <xsl:choose>
                  <xsl:when test="AIDRIPVD:ApplicationForIssuingDocumentForRightsAndImposedPenaltiesOfVehicleDriverData/AIDRIPVDD:ANDCertificateReason = '2006' ">
                    пред  Консултски отдел
                  </xsl:when>
                </xsl:choose>
                <xsl:choose>
                  <xsl:when test="AIDRIPVD:ApplicationForIssuingDocumentForRightsAndImposedPenaltiesOfVehicleDriverData/AIDRIPVDD:ANDCertificateReason = '2007' ">
                    за	Лична информация
                  </xsl:when>
                </xsl:choose>
                <xsl:choose>
                  <xsl:when test="AIDRIPVD:ApplicationForIssuingDocumentForRightsAndImposedPenaltiesOfVehicleDriverData/AIDRIPVDD:ANDCertificateReason = '2008' ">
                    за Преквалификация
                  </xsl:when>
                </xsl:choose>
              </td>
            </tr>
            <tr>
              <td colspan = "2">

              </td>
            </tr>
            <xsl:choose>
              <xsl:when test = "AIDRIPVD:Declarations">
                <xsl:for-each select="AIDRIPVD:Declarations/AIDRIPVD:Declaration">
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
              <xsl:when test = "AIDRIPVD:AttachedDocuments">
                <tr>
                  <td colspan="2">
                    Приложени документи:
                  </td>
                </tr>
                <tr>
                  <td colspan="2">
                    <ol>
                      <xsl:for-each select="AIDRIPVD:AttachedDocuments/AIDRIPVD:AttachedDocument">
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
                Дата:&#160;<xsl:value-of  select="ms:format-date(AIDRIPVD:ElectronicAdministrativeServiceFooter/EASF:ApplicationSigningTime , 'dd.MM.yyyy')"/>г.
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