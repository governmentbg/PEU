<xsl:stylesheet version="1.0" xmlns:RFCO="http://ereg.egov.bg/segment/R-3306"
          xmlns:DTU="http://ereg.egov.bg/segment/0009-000003"
          xmlns:DTN="http://ereg.egov.bg/value/0008-000007"
	        xmlns:DU="http://ereg.egov.bg/segment/0009-000001"
	        xmlns:ACU="http://ereg.egov.bg/segment/0009-000073"
	        xmlns:VC="http://ereg.egov.bg/segment/R-3307"
          xmlns:VR="http://ereg.egov.bg/segment/R-3303"
          xmlns:PD="http://ereg.egov.bg/segment/R-3300"
          xmlns:RD="http://ereg.egov.bg/segment/R-3302"
          xmlns:EA="http://ereg.egov.bg/value/0008-000036"
          xmlns:ESA="http://ereg.egov.bg/segment/0009-000016"
          xmlns:S="http://ereg.egov.bg/segment/R-3301"
          xmlns:DROSD="http://ereg.egov.bg/value/0008-000004"
          xmlns:ABN="http://ereg.egov.bg/value/0008-000047"
          xmlns:XDS="http://ereg.egov.bg/segment/0009-000004"
          xmlns:PBD="http://ereg.egov.bg/segment/0009-000008"
          xmlns:PN="http://ereg.egov.bg/segment/0009-000005"
          xmlns:PFN="http://ereg.egov.bg/value/0008-000008"
          xmlns:PMN="http://ereg.egov.bg/value/0008-000009"
          xmlns:PLN="http://ereg.egov.bg/value/0008-000010"
          xmlns:PI="http://ereg.egov.bg/segment/0009-000006"
          xmlns:IDBD="http://ereg.egov.bg/segment/0009-000099"
          xmlns:IN="http://ereg.egov.bg/value/0008-000145"
          xmlns:MSN="http://ereg.egov.bg/nomenclature/R-1002"
          xmlns:PA="http://ereg.egov.bg/segment/0009-000094"
          xmlns:PDP="http://ereg.egov.bg/segment/R-3037"
          xmlns:ds="http://www.w3.org/2000/09/xmldsig#"
          xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
          xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
          xmlns:ms="urn:schemas-microsoft-com:xslt" xsi:type="xsl:transform" >

  <xsl:output omit-xml-declaration="yes" method="html"/>

  <xsl:template match="RFCO:ReportForChangingOwnership">
    <xsl:text disable-output-escaping='yes'>&lt;!DOCTYPE html&gt;</xsl:text>
    <html>
      <body>
        <table align="center" cellpadding="5" width="700px" >
          <thead>
            <tr>
              <th colspan="4">
                <h3>Справка при смяна на собственост на ПС</h3>
              </th>
            </tr>
          </thead>
          <tbody>
            <tr>
              <td colspan="4">
                <h3>Справки по данни за ПС</h3>
              </td>
            </tr>
            <tr>
              <td colspan="2">
                <b>Регистрационен №:</b>&#160;<xsl:value-of select="RFCO:VehicleRegistrationData/VR:RegistrationNumber" />
              </td>
              <td colspan="2">
                <b>Рама (VIN):</b>&#160;<xsl:value-of select="RFCO:VehicleRegistrationData/VR:IdentificationNumber" />
              </td>
            </tr>
            <tr>
              <td colspan="4">
                <b>Номер на двигател:</b>&#160;<xsl:value-of select="RFCO:VehicleRegistrationData/VR:EngineNumber" />
              </td>
            </tr>

            <tr>
              <td colspan="2">
                <b>Категория на ПС:</b>&#160;<xsl:value-of select="RFCO:VehicleRegistrationData/VR:VehicleCategory/VC:Name" />
              </td>
              <td colspan="2">
                <b>Номер на СРМПС:</b>&#160;<xsl:value-of select="RFCO:VehicleRegistrationData/VR:RegistrationCertificateNumber" />
              </td>
            </tr>
            <tr>
              <td colspan="2">
                <b>Дата на първа регистрация:</b>
                <br/>
                <xsl:value-of select="ms:format-date(RFCO:VehicleRegistrationData/VR:DateOfFirstReg , 'dd.MM.yyyy')"/>
              </td>
              <td colspan="2">
                <b>Дата на следващ технически преглед:</b>
                <br/>
                <xsl:value-of select="ms:format-date(RFCO:VehicleRegistrationData/VR:NextVehicleInspection , 'dd.MM.yyyy')" />
              </td>
            </tr>

            <tr>
              <td colspan="4">
                <b>Структура на МВР, регистрирала ПС:&#160;</b>
                <xsl:value-of  select="RFCO:VehicleRegistrationData/VR:PoliceDepartment/PDP:PoliceDepartmentName"/>
              </td>
            </tr>

            <xsl:if test="RFCO:VehicleRegistrationData/VR:Statuses/VR:Status/S:Blocking ='true'">
              <tr>
                <td colspan="4">
                  <strong>Причини за прекратяване на ЕАУ:</strong>
                </td>
              </tr>
            </xsl:if>

            <xsl:for-each select="RFCO:VehicleRegistrationData/VR:Statuses/VR:Status">
              <xsl:if test="S:Blocking ='true'">
                <tr>
                  <td colspan="4">
                    <xsl:value-of  select="S:Description"/>
                  </td>
                </tr>
              </xsl:if>
            </xsl:for-each>
            <xsl:if test="RFCO:VehicleRegistrationData/VR:Statuses/VR:Status/S:Blocking ='false'">
              <tr>
                <td colspan="4">
                  <strong>СРМПС не може да бъде заявено за печат поради следните причини:</strong>
                </td>
              </tr>
            </xsl:if>
            <xsl:for-each select="RFCO:VehicleRegistrationData/VR:Statuses/VR:Status">
              <xsl:if test="S:Blocking ='false'">
                <tr>
                  <td colspan="4">
                    <xsl:value-of  select="S:Description"/>
                  </td>
                </tr>
              </xsl:if>
            </xsl:for-each>

            <tr>
              <td colspan="4">
                <hr/>
                <br/>
              </td>
            </tr>
            <xsl:if test="RFCO:LocalTaxes/RFCO:Status">
              <tr>
                <td colspan="4">
                  <h3>Справка - Централизирана информационна система за местни данъци и такси „Матеус“</h3>
                </td>
              </tr>
              <xsl:for-each select="RFCO:LocalTaxes/RFCO:Status">
                <tr>
                  <td colspan="4">
                    <xsl:value-of  select="S:Description"/>
                  </td>
                </tr>
              </xsl:for-each>
              <tr>
                <td colspan="4">
                  <hr/>
                  <br/>
                </td>
              </tr>

            </xsl:if>
            <tr>
              <td colspan="4">
                <h3>Справка - Регистър застраховки „Гражданска отговорност (ГО)“</h3>
              </td>
            </tr>
            <xsl:choose>
              <xsl:when test="RFCO:GuaranteeFund/RFCO:Status">
                <tr>
                  <td colspan="4">По данни от Гаранционния фонд, не е платена гражданска отговорност за проверяваното ППС.</td>
                </tr>
              </xsl:when>
              <xsl:otherwise>
                <tr>
                  <td colspan="4">
                    <b>Дата на валидност на полица:&#160;</b>
                    <xsl:value-of  select="ms:format-date(RFCO:GuaranteeFund/RFCO:PolicyValidTo , 'dd.MM.yyyy')"/>
                  </td>
                </tr>
              </xsl:otherwise>
            </xsl:choose>
            <tr>
              <td colspan="4">
                <hr/>
                <br/>
              </td>
            </tr>
            <xsl:if test="RFCO:PeriodicTechnicalCheck/RFCO:Status">
              <tr>
                <td colspan="4">
                  <h3>Справка - Регистър „Периодични технически прегледи“</h3>
                </td>
              </tr>
              <xsl:for-each select="RFCO:PeriodicTechnicalCheck/RFCO:Status">
                <tr>
                  <td colspan="4">
                    <xsl:value-of  select="S:Description"/>
                  </td>
                </tr>
              </xsl:for-each>
              <tr>
                <td colspan="4">
                  <hr/>
                  <br/>
                </td>
              </tr>
            </xsl:if>
            <tr>
              <td colspan="4">
                <h3>Справки по данни за настоящ/и собственик/ци</h3>
              </td>
            </tr>
            <xsl:for-each select="RFCO:OldOwnersData/RFCO:OldOwners/RFCO:PersonData">
              <tr>
                <td colspan="4">
                  <h4>Физически лица в НАИФ НРБЛД</h4>
                </td>
              </tr>
              <xsl:if test="PD:PersonBasicData/PBD:Names/PN:First">
                <tr>
                  <td colspan="2">
                    <b>Имена:</b>&#160;
                    <xsl:value-of select="PD:PersonBasicData/PBD:Names/PN:First"/>&#160;
                    <xsl:value-of select="PD:PersonBasicData/PBD:Names/PN:Middle"/>&#160;
                    <xsl:value-of select="PD:PersonBasicData/PBD:Names/PN:Last"/>
                  </td>
                  <xsl:choose>
                    <xsl:when test="PD:PersonBasicData/PBD:Identifier/PI:EGN">
                      <td colspan="2">
                        <b>ЕГН:</b>&#160;<xsl:value-of  select="PD:PersonBasicData/PBD:Identifier/PI:EGN"/>
                      </td>
                    </xsl:when>
                    <xsl:otherwise>
                      <td colspan="2">
                        <b>ЛНЧ:</b>&#160;<xsl:value-of  select="PD:PersonBasicData/PBD:Identifier/PI:LNCh"/>
                      </td>
                    </xsl:otherwise>
                  </xsl:choose>

                </tr>
              </xsl:if>
              <tr>
                <td colspan="2">
                  <b>Тип на документ:</b>&#160;
                  <xsl:choose>
                    <xsl:when test="PD:PersonBasicData/PBD:IdentityDocument/IDBD:IdentityDocumentType = '0006-000087'">
                      Лична карта
                    </xsl:when>
                  </xsl:choose>
                  <xsl:choose>
                    <xsl:when test="PD:PersonBasicData/PBD:IdentityDocument/IDBD:IdentityDocumentType = '0006-000088'">
                      Паспорт
                    </xsl:when>
                  </xsl:choose>
                  <xsl:choose>
                    <xsl:when test="PD:PersonBasicData/PBD:IdentityDocument/IDBD:IdentityDocumentType = '0006-000089'">
                      Дипломатически паспорт
                    </xsl:when>
                  </xsl:choose>
                  <xsl:choose>
                    <xsl:when test="PD:PersonBasicData/PBD:IdentityDocument/IDBD:IdentityDocumentType = '0006-000090'">
                      Служебен паспорт
                    </xsl:when>
                  </xsl:choose>
                  <xsl:choose>
                    <xsl:when test="PD:PersonBasicData/PBD:IdentityDocument/IDBD:IdentityDocumentType = '0006-000091'">
                      Моряшки паспорт
                    </xsl:when>
                  </xsl:choose>
                  <xsl:choose>
                    <xsl:when test="PD:PersonBasicData/PBD:IdentityDocument/IDBD:IdentityDocumentType = '0006-000092'">
                      Военна карта за самоличност
                    </xsl:when>
                  </xsl:choose>
                  <xsl:choose>
                    <xsl:when test="PD:PersonBasicData/PBD:IdentityDocument/IDBD:IdentityDocumentType = '0006-000093'">
                      Свидетелство за управление на моторно превозно средство
                    </xsl:when>
                  </xsl:choose>
                  <xsl:choose>
                    <xsl:when test="PD:PersonBasicData/PBD:IdentityDocument/IDBD:IdentityDocumentType = '0006-000094'">
                      Временен паспорт
                    </xsl:when>
                  </xsl:choose>
                  <xsl:choose>
                    <xsl:when test="PD:PersonBasicData/PBD:IdentityDocument/IDBD:IdentityDocumentType = '0006-000095'">
                      Служебен открит лист за преминаване на границата
                    </xsl:when>
                  </xsl:choose>
                  <xsl:choose>
                    <xsl:when test="PD:PersonBasicData/PBD:IdentityDocument/IDBD:IdentityDocumentType = '0006-000097'">
                      Карта на бежанец
                    </xsl:when>
                  </xsl:choose>
                  <xsl:choose>
                    <xsl:when test="PD:PersonBasicData/PBD:IdentityDocument/IDBD:IdentityDocumentType = '0006-000099'">
                      Карта на чужденец с хуманитарен статут
                    </xsl:when>
                  </xsl:choose>
                  <xsl:choose>
                    <xsl:when test="PD:PersonBasicData/PBD:IdentityDocument/IDBD:IdentityDocumentType = '0006-000098'">
                      Карта на чужденец, получил убежище
                    </xsl:when>
                  </xsl:choose>
                  <xsl:choose>
                    <xsl:when test="PD:PersonBasicData/PBD:IdentityDocument/IDBD:IdentityDocumentType = '0006-000121'">
                      Разрешение за пребиваване
                    </xsl:when>
                  </xsl:choose>
                  <xsl:choose>
                    <xsl:when test="PD:PersonBasicData/PBD:IdentityDocument/IDBD:IdentityDocumentType = '0006-000122'">
                      Удостоверение за пребиваване на гражданин на ЕС
                    </xsl:when>
                  </xsl:choose>
                </td>
                <td colspan="2">
                  <b>Документ за самоличност №:</b>&#160;<xsl:value-of  select="PD:PersonBasicData/PBD:IdentityDocument/IDBD:IdentityNumber"/>
                </td>
              </tr>

              <xsl:if test="PD:PersonBasicData/PBD:IdentityDocument/IDBD:IdentitityIssueDate != '0001-01-01'">
                <tr>
                  <td colspan="2">
                    <b>Издател:</b>&#160;<xsl:value-of  select="PD:PersonBasicData/PBD:IdentityDocument/IDBD:IdentityIssuer"/>
                  </td>
                  <td colspan="2">
                    <b>Издаден на:</b>&#160;<xsl:value-of  select="ms:format-date(PD:PersonBasicData/PBD:IdentityDocument/IDBD:IdentitityIssueDate , 'dd.MM.yyyy')"/>
                  </td>
                </tr>
              </xsl:if>
              <tr>
                <td colspan="2">
                  <b>Семейно положение:</b>&#160;
                  <xsl:choose>
                    <xsl:when test="PD:MaritalStatus = 355">вдовец/вдовица</xsl:when>
                    <xsl:when test="PD:MaritalStatus = 356">неженен/неомъжена</xsl:when>
                    <xsl:when test="PD:MaritalStatus = 357">женен/омъжена</xsl:when>
                    <xsl:when test="PD:MaritalStatus = 358">разведен(а)</xsl:when>
                    <xsl:when test="PD:MaritalStatus = 359">факт.разделен(а)</xsl:when>
                    <xsl:when test="PD:MaritalStatus = 4832">непоказано</xsl:when>
                  </xsl:choose>
                </td>
                <xsl:if test="ms:format-date(PD:DeathDate , 'dd.MM.yyyy') != ''">
                  <td colspan="2">
                    <b>Дата на смърт:</b>&#160;<xsl:value-of  select="ms:format-date(PD:DeathDate , 'dd.MM.yyyy')"/>
                  </td>
                </xsl:if>
              </tr>
              <tr>
                <td colspan="4">
                  <xsl:choose>
                    <xsl:when test="ms:format-date(PD:DeathDate , 'dd.MM.yyyy') != ''">Моля извършете проверка в ЕСГРАОН за достоверност на данните за починало лице.</xsl:when>
                    <xsl:otherwise>Данните за семейно положение са информативни. Моля извършете проверка за актуално семейно положение в ЕСГРАОН.</xsl:otherwise>
                  </xsl:choose>
                </td>
              </tr>
              <tr>
                <td colspan="4">
                  <b>Постоянен адрес:</b>
                </td>
              </tr>
              <tr>
                <td colspan="2">
                  <b>Област:</b>&#160;<xsl:value-of  select="PD:PermanentAddress/PA:DistrictGRAOName"/>
                </td>
                <td colspan="2">
                  <b>Община:</b>&#160;<xsl:value-of  select="PD:PermanentAddress/PA:MunicipalityGRAOName"/>
                </td>
              </tr>
              <tr>
                <td colspan="2">
                  <b>Населено място:</b>&#160;<xsl:value-of  select="PD:PermanentAddress/PA:SettlementGRAOName"/>
                </td>
                <td colspan="2">
                  <b>ул./бул./пл./ж.к./кв.:</b>&#160;<xsl:value-of  select="PD:PermanentAddress/PA:StreetText"/>
                </td>
              </tr>
              <tr>
                <td colspan="2">
                  <b>Номер на сграда:</b>&#160;<xsl:value-of  select="PD:PermanentAddress/PA:BuildingNumber"/>
                </td>
                <td colspan="2">
                  <b>Вход:</b>&#160;<xsl:value-of  select="PD:PermanentAddress/PA:Entrance"/>
                </td>
              </tr>
              <tr>
                <td colspan="2">
                  <b>Етаж:</b>&#160;<xsl:value-of  select="PD:PermanentAddress/PA:Floor"/>
                </td>
                <td colspan="2">
                  <b>Апартамент:</b>&#160;<xsl:value-of  select="PD:PermanentAddress/PA:Apartment"/>
                </td>
              </tr>
              <xsl:if test="PD:Status/S:Description">
                <tr>
                  <td colspan="4">
                    <b>Статус:</b>&#160;<xsl:value-of  select="PD:Status/S:Description"/>
                  </td>
                </tr>
              </xsl:if>
              <tr>
                <td colspan="4">
                  <hr/>
                  <br/>
                </td>
              </tr>
            </xsl:for-each>
            <xsl:for-each select="RFCO:OldOwnersData/RFCO:OldOwners/RFCO:EntityData">
              <tr>
                <td colspan="4">
                  <h4>Юридически лица в Търговски регистър/ Регистър „Булстат“</h4>
                </td>
              </tr>
              <tr>
                <td colspan="2">
                  <b>ЕИК/БУЛСТАТ:</b>&#160;<xsl:value-of  select="RD:Identifier"/>
                </td>
                <td colspan="2">
                  <b>Кратко наименование:</b>&#160;<xsl:value-of  select="RD:Name"/>
                </td>
              </tr>
              <tr>
                <td colspan="2">
                  <b>Пълно наименование:</b>&#160;<xsl:value-of  select="RD:FullName"/>
                </td>
                <td colspan="2">
                  <b>Наименование на латиница:</b>&#160;<xsl:value-of  select="RD:NameTrans"/>
                </td>
              </tr>
              <xsl:if test="RD:RecStatus">
                <tr>
                  <td colspan="4">
                    <b>Актуално състояние:</b>
                  </td>
                </tr>
                <tr>
                  <td colspan="4">
                    <xsl:value-of  select="RD:RecStatus"/>
                  </td>
                </tr>
              </xsl:if>
              <tr>
                <td colspan="4">
                  <b>Адрес на управление:</b>
                </td>
              </tr>
              <tr>
                <td colspan="2">
                  <b>Област:</b>&#160;<xsl:value-of  select="RD:EntityManagmentAddress/PA:DistrictGRAOName"/>
                </td>
                <td colspan="2">
                  <b>Община:</b>&#160;<xsl:value-of  select="RD:EntityManagmentAddress/PA:MunicipalityGRAOName"/>
                </td>
              </tr>
              <tr>
                <td colspan="2">
                  <b>Населено място:</b>&#160;<xsl:value-of  select="RD:EntityManagmentAddress/PA:SettlementGRAOName"/>
                </td>
                <td colspan="2">
                  <b>ул./бул./пл./ж.к./кв.:</b>&#160;<xsl:value-of  select="RD:EntityManagmentAddress/PA:StreetText"/>
                </td>
              </tr>
              <tr>
                <td colspan="2">
                  <b>Номер на сграда:</b>&#160;<xsl:value-of  select="RD:EntityManagmentAddress/PA:BuildingNumber"/>
                </td>
                <td colspan="2">
                  <b>Вход:</b>&#160;<xsl:value-of  select="RD:EntityManagmentAddress/PA:Entrance"/>
                </td>
              </tr>
              <tr>
                <td colspan="2">
                  <b>Етаж:</b>&#160;<xsl:value-of  select="RD:EntityManagmentAddress/PA:Floor"/>
                </td>
                <td colspan="2">
                  <b>Апартамент:</b>&#160;<xsl:value-of  select="RD:EntityManagmentAddress/PA:Apartment"/>
                </td>
              </tr>
              <xsl:if test="RD:Status/S:Description">
                <tr>
                  <td>
                    <b>Статус:</b>
                    <xsl:value-of  select="RD:Status/S:Description"/>
                  </td>
                </tr>
              </xsl:if>
              <tr>
                <td colspan="4">
                  <hr/>
                  <br/>
                </td>
              </tr>
            </xsl:for-each>
            <tr>
              <td colspan="4">
                <h3>Справки по данни за нов/и собственик/ци</h3>
              </td>
              <tr/>
              <xsl:for-each select="RFCO:NewOwnersData/RFCO:NewOwners/RFCO:PersonData">
                <tr>
                  <td colspan="4">
                    <h4>Физически лица в НАИФ НРБЛД</h4>
                  </td>
                </tr>
                <xsl:if test="PD:PersonBasicData/PBD:Names/PN:First">
                  <tr>
                    <td colspan="2">
                      <b>Имена:</b>&#160;
                      <xsl:value-of select="PD:PersonBasicData/PBD:Names/PN:First"/>&#160;
                      <xsl:value-of select="PD:PersonBasicData/PBD:Names/PN:Middle"/>&#160;
                      <xsl:value-of select="PD:PersonBasicData/PBD:Names/PN:Last"/>
                    </td>
                    <xsl:choose>
                      <xsl:when test="PD:PersonBasicData/PBD:Identifier/PI:EGN">
                        <td colspan="2">
                          <b>ЕГН:</b>&#160;<xsl:value-of  select="PD:PersonBasicData/PBD:Identifier/PI:EGN"/>
                        </td>
                      </xsl:when>
                      <xsl:otherwise>
                        <td colspan="2">
                          <b>ЛНЧ:</b>&#160;<xsl:value-of  select="PD:PersonBasicData/PBD:Identifier/PI:LNCh"/>
                        </td>
                      </xsl:otherwise>
                    </xsl:choose>
                  </tr>
                </xsl:if>
                <tr>
                  <td colspan="2">
                    <b>Тип на документ:</b>&#160;
                    <xsl:choose>
                      <xsl:when test="PD:PersonBasicData/PBD:IdentityDocument/IDBD:IdentityDocumentType = '0006-000087'">
                        Лична карта
                      </xsl:when>
                    </xsl:choose>
                    <xsl:choose>
                      <xsl:when test="PD:PersonBasicData/PBD:IdentityDocument/IDBD:IdentityDocumentType = '0006-000088'">
                        Паспорт
                      </xsl:when>
                    </xsl:choose>
                    <xsl:choose>
                      <xsl:when test="PD:PersonBasicData/PBD:IdentityDocument/IDBD:IdentityDocumentType = '0006-000089'">
                        Дипломатически паспорт
                      </xsl:when>
                    </xsl:choose>
                    <xsl:choose>
                      <xsl:when test="PD:PersonBasicData/PBD:IdentityDocument/IDBD:IdentityDocumentType = '0006-000090'">
                        Служебен паспорт
                      </xsl:when>
                    </xsl:choose>
                    <xsl:choose>
                      <xsl:when test="PD:PersonBasicData/PBD:IdentityDocument/IDBD:IdentityDocumentType = '0006-000091'">
                        Моряшки паспорт
                      </xsl:when>
                    </xsl:choose>
                    <xsl:choose>
                      <xsl:when test="PD:PersonBasicData/PBD:IdentityDocument/IDBD:IdentityDocumentType = '0006-000092'">
                        Военна карта за самоличност
                      </xsl:when>
                    </xsl:choose>
                    <xsl:choose>
                      <xsl:when test="PD:PersonBasicData/PBD:IdentityDocument/IDBD:IdentityDocumentType = '0006-000093'">
                        Свидетелство за управление на моторно превозно средство
                      </xsl:when>
                    </xsl:choose>
                    <xsl:choose>
                      <xsl:when test="PD:PersonBasicData/PBD:IdentityDocument/IDBD:IdentityDocumentType = '0006-000094'">
                        Временен паспорт
                      </xsl:when>
                    </xsl:choose>
                    <xsl:choose>
                      <xsl:when test="PD:PersonBasicData/PBD:IdentityDocument/IDBD:IdentityDocumentType = '0006-000095'">
                        Служебен открит лист за преминаване на границата
                      </xsl:when>
                    </xsl:choose>
                    <xsl:choose>
                      <xsl:when test="PD:PersonBasicData/PBD:IdentityDocument/IDBD:IdentityDocumentType = '0006-000097'">
                        Карта на бежанец
                      </xsl:when>
                    </xsl:choose>
                    <xsl:choose>
                      <xsl:when test="PD:PersonBasicData/PBD:IdentityDocument/IDBD:IdentityDocumentType = '0006-000099'">
                        Карта на чужденец с хуманитарен статут
                      </xsl:when>
                    </xsl:choose>
                    <xsl:choose>
                      <xsl:when test="PD:PersonBasicData/PBD:IdentityDocument/IDBD:IdentityDocumentType = '0006-000098'">
                        Карта на чужденец, получил убежище
                      </xsl:when>
                    </xsl:choose>
                    <xsl:choose>
                      <xsl:when test="PD:PersonBasicData/PBD:IdentityDocument/IDBD:IdentityDocumentType = '0006-000121'">
                        Разрешение за пребиваване
                      </xsl:when>
                    </xsl:choose>
                    <xsl:choose>
                      <xsl:when test="PD:PersonBasicData/PBD:IdentityDocument/IDBD:IdentityDocumentType = '0006-000122'">
                        Удостоверение за пребиваване на гражданин на ЕС
                      </xsl:when>
                    </xsl:choose>

                  </td>
                  <td colspan="2">
                    <b>Документ за самоличност №:</b>&#160;<xsl:value-of  select="PD:PersonBasicData/PBD:IdentityDocument/IDBD:IdentityNumber"/>
                  </td>
                </tr>
                <xsl:if test="PD:PersonBasicData/PBD:IdentityDocument/IDBD:IdentitityIssueDate != '0001-01-01'">
                  <tr>
                    <td colspan="2">
                      <b>Издател:</b>&#160;<xsl:value-of  select="PD:PersonBasicData/PBD:IdentityDocument/IDBD:IdentityIssuer"/>
                    </td>
                    <td colspan="2">
                      <b>Издаден на:</b>&#160;<xsl:value-of  select="ms:format-date(PD:PersonBasicData/PBD:IdentityDocument/IDBD:IdentitityIssueDate , 'dd.MM.yyyy')"/>
                    </td>
                  </tr>
                </xsl:if>
                <tr>
                  <td colspan="2">
                    <b>Семейно положение:</b>&#160;
                    <xsl:choose>
                      <xsl:when test="PD:MaritalStatus = 355">вдовец/вдовица</xsl:when>
                      <xsl:when test="PD:MaritalStatus = 356">неженен/неомъжена</xsl:when>
                      <xsl:when test="PD:MaritalStatus = 357">женен/омъжена</xsl:when>
                      <xsl:when test="PD:MaritalStatus = 358">разведен(а)</xsl:when>
                      <xsl:when test="PD:MaritalStatus = 359">факт.разделен(а)</xsl:when>
                      <xsl:when test="PD:MaritalStatus = 4832">непоказано</xsl:when>
                    </xsl:choose>
                  </td>
                  <xsl:if test="ms:format-date(PD:DeathDate , 'dd.MM.yyyy') != ''">
                    <td colspan="2">
                      <b>Дата на смърт:</b>&#160;<xsl:value-of  select="ms:format-date(PD:DeathDate , 'dd.MM.yyyy')"/>
                    </td>
                  </xsl:if>
                </tr>
                <tr>
                  <td colspan="4">
                    <xsl:choose>
                      <xsl:when test="ms:format-date(PD:DeathDate , 'dd.MM.yyyy') != ''">Моля извършете проверка в ЕСГРАОН за достоверност на данните за починало лице.</xsl:when>
                      <xsl:otherwise>Данните за семейно положение са информативни. Моля извършете проверка за актуално семейно положение в ЕСГРАОН.</xsl:otherwise>
                    </xsl:choose>

                  </td>
                </tr>
                <tr>
                  <td colspan="4">
                    <b>Постоянен адрес:</b>
                  </td>
                </tr>
                <tr>
                  <td colspan="2">
                    <b>Област:</b>&#160;<xsl:value-of  select="PD:PermanentAddress/PA:DistrictGRAOName"/>
                  </td>
                  <td colspan="2">
                    <b>Община:</b>&#160;<xsl:value-of  select="PD:PermanentAddress/PA:MunicipalityGRAOName"/>
                  </td>
                </tr>
                <tr>
                  <td colspan="2">
                    <b>Населено място:</b>&#160;<xsl:value-of  select="PD:PermanentAddress/PA:SettlementGRAOName"/>
                  </td>
                  <td colspan="2">
                    <b>ул./бул./пл./ж.к./кв.:</b>&#160;<xsl:value-of  select="PD:PermanentAddress/PA:StreetText"/>
                  </td>
                </tr>
                <tr>
                  <td colspan="2">
                    <b>Номер на сграда:</b>&#160;<xsl:value-of  select="PD:PermanentAddress/PA:BuildingNumber"/>
                  </td>
                  <td colspan="2">
                    <b>Вход:</b>&#160;<xsl:value-of  select="PD:PermanentAddress/PA:Entrance"/>
                  </td>
                </tr>
                <tr>
                  <td colspan="2">
                    <b>Етаж:</b>&#160;<xsl:value-of  select="PD:PermanentAddress/PA:Floor"/>
                  </td>
                  <td colspan="2">
                    <b>Апартамент:</b>&#160;<xsl:value-of  select="PD:PermanentAddress/PA:Apartment"/>
                  </td>
                </tr>
                <xsl:if test="PD:Status/S:Description">
                  <tr>
                    <td colspan="4">
                      <b>Статус:</b>&#160;<xsl:value-of  select="PD:Status/S:Description"/>
                    </td>
                  </tr>
                </xsl:if>
                <tr>
                  <td colspan="4">
                    <hr/>
                    <br/>
                  </td>
                </tr>
              </xsl:for-each>
              <xsl:for-each select="RFCO:NewOwnersData/RFCO:NewOwners/RFCO:EntityData">
                <tr>
                  <td colspan="4">
                    <h4>Юридически лица в Търговски регистър/ Регистър „Булстат“</h4>
                  </td>
                </tr>
                <tr>
                  <td colspan="2">
                    <b>ЕИК/БУЛСТАТ:</b>&#160;<xsl:value-of  select="RD:Identifier"/>
                  </td>
                  <td colspan="2">
                    <b>Кратко наименование:</b>&#160;<xsl:value-of  select="RD:Name"/>
                  </td>
                </tr>
                <tr>
                  <td colspan="2">
                    <b>Пълно наименование:</b>&#160;<xsl:value-of  select="RD:FullName"/>
                  </td>
                  <td colspan="2">
                    <b>Наименование на латиница:</b>&#160;<xsl:value-of  select="RD:NameTrans"/>
                  </td>
                </tr>
                <xsl:if test="RD:RecStatus">
                  <tr>
                    <td colspan="4">
                      <b>Актуално състояние:</b>
                    </td>
                  </tr>
                  <tr>
                    <td colspan="4">
                      <xsl:value-of  select="RD:RecStatus"/>
                    </td>
                  </tr>
                </xsl:if>
                <tr>
                  <td colspan="4">
                    <b>Адрес на управление:</b>
                  </td>
                </tr>

                <tr>
                  <td colspan="2">
                    <b>Област:</b>&#160;<xsl:value-of  select="RD:EntityManagmentAddress/PA:DistrictGRAOName"/>
                  </td>
                  <td colspan="2">
                    <b>Община:</b>&#160;<xsl:value-of  select="RD:EntityManagmentAddress/PA:MunicipalityGRAOName"/>
                  </td>
                </tr>
                <tr>
                  <td colspan="2">
                    <b>Населено място:</b>&#160;<xsl:value-of  select="RD:EntityManagmentAddress/PA:SettlementGRAOName"/>
                  </td>
                  <td colspan="2">
                    <b>ул./бул./пл./ж.к./кв.:</b>&#160;<xsl:value-of  select="RD:EntityManagmentAddress/PA:StreetText"/>
                  </td>
                </tr>
                <tr>
                  <td colspan="2">
                    <b>Номер на сграда:</b>&#160;<xsl:value-of  select="RD:EntityManagmentAddress/PA:BuildingNumber"/>
                  </td>
                  <td colspan="2">
                    <b>Вход:</b>&#160;<xsl:value-of  select="RD:EntityManagmentAddress/PA:Entrance"/>
                  </td>
                </tr>
                <tr>
                  <td colspan="2">
                    <b>Етаж:</b>&#160;<xsl:value-of  select="RD:EntityManagmentAddress/PA:Floor"/>
                  </td>
                  <td colspan="2">
                    <b>Апартамент:</b>&#160;<xsl:value-of  select="RD:EntityManagmentAddress/PA:Apartment"/>
                  </td>
                </tr>
                <xsl:if test="RD:Status/S:Description">
                  <tr>
                    <td>
                      <b>Статус:</b>
                      <xsl:value-of  select="RD:Status/S:Description"/>
                    </td>
                  </tr>
                </xsl:if>
                <tr>
                  <td colspan="4">
                    <hr/>
                    <br/>
                  </td>
                </tr>
              </xsl:for-each>
            </tr>
          </tbody>
        </table>
      </body>
    </html>
  </xsl:template>
</xsl:stylesheet>