<xsl:stylesheet version="1.0" xmlns:MOI="http://ereg.egov.bg/segment/R-3002"
                xmlns:ds="http://www.w3.org/2000/09/xmldsig#"
                xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
                xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
                xmlns:APIDBS="http://ereg.egov.bg/segment/R-3006"
                xmlns:PI="http://ereg.egov.bg/segment/R-3005"
                xmlns:ID="http://ereg.egov.bg/segment/R-3004"
                xmlns:NM="http://ereg.egov.bg/segment/0009-000005"
                xmlns:NMLN="http://ereg.egov.bg/segment/R-3003"
                xmlns:ADR="http://ereg.egov.bg/segment/0009-000094"
                xmlns:GENDER="http://ereg.egov.bg/segment/0009-000156"
                xmlns:SPOUSE="http://ereg.egov.bg/segment/0009-000135"
                xmlns:SPDATA="http://ereg.egov.bg/segment/0009-000008"
                xmlns:SPNM ="http://ereg.egov.bg/segment/0009-000005"
                xmlns:CRBD="http://ereg.egov.bg/segment/0009-000110"
				xmlns:SARD="http://ereg.egov.bg/segment/0009-000141"
				        xmlns:IPAS="http://ereg.egov.bg/segment/R-3043"
				        xmlns:EASH="http://ereg.egov.bg/segment/0009-000152"
				        xmlns:PDC="http://ereg.egov.bg/segment/R-3037"
				        xmlns:DURI="http://ereg.egov.bg/segment/0009-000001"
				        xmlns:CN="http://ereg.egov.bg/segment/R-3020"
				        xmlns:CH="http://ereg.egov.bg/segment/0009-000133"
				        xmlns:IDBD="http://ereg.egov.bg/segment/0009-000099"
				        xmlns:ESA="http://ereg.egov.bg/segment/0009-000016"
				        xmlns:REC="http://ereg.egov.bg/segment/0009-000015"
				        xmlns:P="http://ereg.egov.bg/segment/0009-000008"
                xmlns:DECL="http://ereg.egov.bg/segment//R-3136"
                xmlns:EASF="http://ereg.egov.bg/segment/0009-000153"
                xmlns:xslExtension="urn:XSLExtension"
                xmlns:ms="urn:schemas-microsoft-com:xslt" xsi:type="xsl:transform" >

	<xsl:include href="./BDSBaseTemplates.xslt"/>

	<xsl:output omit-xml-declaration="yes" method="html"/>
	<xsl:template match="MOI:ApplicationForIssuanceOfPersonalDocumentsOfBulgarianCitizens">
		<xsl:text disable-output-escaping='yes'>&lt;!DOCTYPE html&gt;</xsl:text>
		<html>
			<xsl:call-template name="Head"/>
			<body>
				<table border="0" cellspacing="0" width="100%" style="font-family: sans-serif; font-size: 9px;horiz-align: left ; ">
					<thead width="100%">

						<tr>
							<td style="border: none;" rowspan="3" align="center">
								<xsl:choose>
									<xsl:when test="MOI:IdentificationPhotoAndSignature/IPAS:IdentificationPhoto">
										<div width="200" height="300">
											<img  width="150" height="200">
												<xsl:attribute name="src" >
													<xsl:value-of select="concat('data:image/gif;base64,',MOI:IdentificationPhotoAndSignature/IPAS:IdentificationPhoto)"/>
												</xsl:attribute>
											</img>
										</div>
									</xsl:when>
									<xsl:otherwise>
										<div width="200" height="300">
											&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;<br/>&#160;<br/>&#160;<br/>&#160;<br/>&#160;<br/>&#160;<br/>&#160;<br/>&#160;<br/>&#160;<br/>&#160;<br/>&#160;<br/>&#160;<br/>&#160;
										</div>
									</xsl:otherwise>
								</xsl:choose>
								<br/>
								<br />
								<xsl:choose>
									<xsl:when test="MOI:ApplicationForIssuanceOfPersonalDocumentsOfBulgarianCitizensData/APIDBS:IdentificationPhotoAndSignature/IPAS:IdentificationSignature/.">
										<xsl:value-of select="MOI:ApplicationForIssuanceOfPersonalDocumentsOfBulgarianCitizensData/APIDBS:IdentificationPhotoAndSignature/IPAS:IdentificationSignature/." />
									</xsl:when>
									<xsl:otherwise>
										<div width="160" height="60">
											&#160;<br/>&#160;<br/>&#160;<br/>&#160;
										</div>
									</xsl:otherwise>
								</xsl:choose>
							</td>
						</tr>
						<tr style="border: none;">
							<th style="border: none; font-size: 14px;">
								ЗАЯВЛЕНИЕ<br/> за издаване на документи за самоличност на български граждани
							</th>
							<td>
								&#160;
							</td>
						</tr>
						<tr>
							<td style="padding: 0px;" colspan="2">
								<table border="1" cellspacing="0" style="width:100%; height: 100%;border: solid 1px black;border-collapse: collapse;font-size: 9px; ">
									<tr>
										<td>
											Вх.номер: <br />
											&#160;
										</td>
										<td>
											До <br />
											<xsl:value-of select="MOI:IssuingPoliceDepartment/PDC:PoliceDepartmentName/." />
											<!--<xsl:for-each select="MOI:ApplicationForIssuanceOfPersonalDocumentsOfBulgarianCitizensData">
                          <xsl:value-of select="APIDBS:PoliceDepartmentName/." />
                        </xsl:for-each>-->
										</td>
									</tr>
									<tr>
										<td>

											<xsl:value-of select="MOI:ApplicationForIssuanceOfPersonalDocumentsOfBulgarianCitizensData/APIDBS:Person/PI:PersonIdentification/ID:Identifier/."/> <br />ЕГН
										</td>
										<td rowspan="2">
											<table border="0" cellspacing="0" style="padding-bottom: 0px;font-size: 9px;">
												<tr>
													<td colspan="2">
														<em>Вид на услугата:</em>
													</td>
												</tr>
												<tr>
													<td colspan="2">
														<xsl:choose>
															<xsl:when test="MOI:ServiceTermType='0006-000083'">
																обикновена <input type="checkbox" checked="true" disabled="" readonly="" /> бърза <input type="checkbox" disabled="" readonly="" /> експресна <input type="checkbox" disabled="" readonly="" />
															</xsl:when>
															<xsl:when test="MOI:ServiceTermType='0006-000084'">
																обикновена <input type="checkbox" disabled="" readonly="" /> бърза <input type="checkbox" checked="true" disabled="" readonly="" /> експресна <input type="checkbox" disabled="" readonly="" />
															</xsl:when>
															<xsl:when test="MOI:ServiceTermType='0006-000085'">
																обикновена <input type="checkbox" disabled="" readonly="" /> бърза <input type="checkbox" disabled="" readonly="" /> експресна <input type="checkbox" checked="true" disabled="" readonly="" />
															</xsl:when>
															<xsl:otherwise>
																обикновена <input type="checkbox" disabled="" readonly="" /> бърза <input type="checkbox" disabled="" readonly="" /> експресна <input type="checkbox" disabled="" readonly="" />
															</xsl:otherwise>
														</xsl:choose>
													</td>
												</tr>
												<tr>
													<td colspan="2">
														<em>Вид на искания документ :</em>
													</td>
												</tr>
												<tr>
													<td>
														<xsl:choose>
															<xsl:when test="MOI:ApplicationForIssuanceOfPersonalDocumentsOfBulgarianCitizensData/APIDBS:IdentificationDocuments/APIDBS:IdentificationDocumentType='0006-000087'">
																<input type="checkbox" checked="true" disabled="" readonly="" />
															</xsl:when>
															<xsl:otherwise>
																<input type="checkbox"  disabled="" readonly="" />
															</xsl:otherwise>
														</xsl:choose>
														лична карта
													</td>
													<td>
														<xsl:choose>
															<xsl:when test="MOI:ApplicationForIssuanceOfPersonalDocumentsOfBulgarianCitizensData/APIDBS:IdentificationDocuments/APIDBS:IdentificationDocumentType='0006-000093'">
																<input type="checkbox" checked="true" disabled="" readonly="" />
															</xsl:when>
															<xsl:otherwise>
																<input type="checkbox"  disabled="" readonly="" />
															</xsl:otherwise>
														</xsl:choose>
														временна карта за самоличност
													</td>
												</tr>
												<tr>
													<td>
														<xsl:choose>
															<xsl:when test="MOI:ApplicationForIssuanceOfPersonalDocumentsOfBulgarianCitizensData/APIDBS:IdentificationDocuments/APIDBS:IdentificationDocumentType='0006-000088'">
																<input type="checkbox" checked="true" disabled="" readonly="" />
															</xsl:when>
															<xsl:otherwise>
																<input type="checkbox"  disabled="" readonly="" />
															</xsl:otherwise>
														</xsl:choose>
														паспорт
													</td>
													<td>
														<input type="checkbox" disabled="" readonly="" />временен паспорт
													</td>
												</tr>
												<tr>
													<td>
														<input type="checkbox" disabled="" readonly="" />дипломатически паспорт
													</td>
													<td>
														<input type="checkbox" disabled="" readonly="" />служебен открит лист за преминаване на границата
													</td>
												</tr>
												<tr>
													<td>
														<input type="checkbox" disabled="" readonly="" />служебен паспорт
													</td>
													<td rowspan="2">
														<input type="checkbox" disabled="" readonly="" />временен паспорт за окончателно напускане на РБ
													</td>
												</tr>
												<tr>
													<td>
														<input type="checkbox" disabled="" readonly="" />моряшки паспорт
													</td>

												</tr>
											</table>
										</td>
									</tr>
									<tr>
										<td align="left">
											Дата на раждане: <br/>
											<xsl:value-of select="ms:format-date(MOI:ApplicationForIssuanceOfPersonalDocumentsOfBulgarianCitizensData/APIDBS:Person/PI:PersonIdentification/ID:BirthDate/. , 'dd.MM.yyyy') "/> г.
										</td>

									</tr>
									<tr>
										<td colspan="2" align="center">
											&#160;<br/>&#160;<br/>
											<xsl:value-of select="MOI:ApplicationForIssuanceOfPersonalDocumentsOfBulgarianCitizensData/APIDBS:Person/PI:PlaceOfBirthOtherData/. " />
											&#160;<br/>&#160;<br/>&#160;&#160;&#160;Място на раждане: (Населено място, община, област или държава)
										</td>

									</tr>

								</table>
							</td>
						</tr>
					</thead>
					<tbody width="100%">
						<tr>
							<td colspan="3" style="padding-top : 0;">
								<table width="100%" align="center" style="border-collapse: collapse;font-size: 9px;" border="1" cellspacing="0">
									<tr>
										<td align="center" colspan="4">
											<table width="100%" style="font-size: 9px;">
												<tr>
													<td style=" border: solid 1px black; text-align: center; width: 90%;">
														<xsl:value-of  select="MOI:ApplicationForIssuanceOfPersonalDocumentsOfBulgarianCitizensData/APIDBS:Person/PI:PersonIdentification/ID:Names/NM:First/."/>
														&#160;
														<xsl:value-of  select="MOI:ApplicationForIssuanceOfPersonalDocumentsOfBulgarianCitizensData/APIDBS:Person/PI:PersonIdentification/ID:Names/NM:Middle/."/>
														&#160;
														<xsl:value-of  select="MOI:ApplicationForIssuanceOfPersonalDocumentsOfBulgarianCitizensData/APIDBS:Person/PI:PersonIdentification/ID:Names/NM:Last/."/>
													</td>
												</tr>
												<tr>
													<td style="width: 95%; text-align: center;">
														Имена на кирилица (име, презиме, фамилия)
													</td>
												</tr>
												<tr>
													<td style="text-align: center; width: 90%;" class="dottedLine">
														<xsl:value-of  select="MOI:ApplicationForIssuanceOfPersonalDocumentsOfBulgarianCitizensData/APIDBS:ChangedNames/CN:PersonNames/NM:First/."/>
														&#160;
														<xsl:value-of  select="MOI:ApplicationForIssuanceOfPersonalDocumentsOfBulgarianCitizensData/APIDBS:ChangedNames/CN:PersonNames/NM:Middle/."/>
														&#160;
														<xsl:value-of  select="MOI:ApplicationForIssuanceOfPersonalDocumentsOfBulgarianCitizensData/APIDBS:ChangedNames/CN:PersonNames/NM:Last/."/>

													</td>
												</tr>
												<tr>
													<td style="width: 95%; text-align: center;">
														Променени имена на кирилица (име, презиме, фамилия)
													</td>
												</tr>
												<tr>
													<td style=" border: solid 1px black; text-align: center; width: 90%;">
														<xsl:value-of  select="MOI:ApplicationForIssuanceOfPersonalDocumentsOfBulgarianCitizensData/APIDBS:Person/PI:PersonIdentification/ID:NamesLatin/NMLN:First/."/>
														&#160;
														<xsl:value-of  select="MOI:ApplicationForIssuanceOfPersonalDocumentsOfBulgarianCitizensData/APIDBS:Person/PI:PersonIdentification/ID:NamesLatin/NMLN:Middle/."/>
														&#160;
														<xsl:value-of  select="MOI:ApplicationForIssuanceOfPersonalDocumentsOfBulgarianCitizensData/APIDBS:Person/PI:PersonIdentification/ID:NamesLatin/NMLN:Last/."/>
													</td>
												</tr>
												<tr>
													<td style="width: 95%; text-align: center;">
														Имена на латиница (име, презиме, фамилия)
													</td>
												</tr>
												<tr>
													<td style="text-align: center; width: 90%;" class="dottedLine">
														<xsl:value-of  select="MOI:ApplicationForIssuanceOfPersonalDocumentsOfBulgarianCitizensData/APIDBS:ChangedNames/CN:PersonNamesLatin/NMLN:First/."/>
														&#160;
														<xsl:value-of  select="MOI:ApplicationForIssuanceOfPersonalDocumentsOfBulgarianCitizensData/APIDBS:ChangedNames/CN:PersonNamesLatin/NMLN:Middle/."/>
														&#160;
														<xsl:value-of  select="MOI:ApplicationForIssuanceOfPersonalDocumentsOfBulgarianCitizensData/APIDBS:ChangedNames/CN:PersonNamesLatin/NMLN:Last/."/>
													</td>
												</tr>
												<tr>
													<td style="width: 95%; text-align: center;">
														Променени имена на латиница (име, презиме, фамилия)
													</td>
												</tr>
											</table>
										</td>
									</tr>

									<tr style="border:1px solid black;">
										<td align="center" colspan="4">
											<table width="100%" style="font-size:9px;">
												<tr>
													<td style="border: solid 1px black; text-align: center; width: 90%;">
														област <xsl:value-of  select="MOI:ApplicationForIssuanceOfPersonalDocumentsOfBulgarianCitizensData/APIDBS:PermanentAddress/ADR:DistrictGRAOName/."/>,&#160;
														община <xsl:value-of  select="MOI:ApplicationForIssuanceOfPersonalDocumentsOfBulgarianCitizensData/APIDBS:PermanentAddress/ADR:MunicipalityGRAOName/."/>,&#160;
														населено място <xsl:value-of  select="MOI:ApplicationForIssuanceOfPersonalDocumentsOfBulgarianCitizensData/APIDBS:PermanentAddress/ADR:SettlementGRAOName/."/>,&#160;
														ул. <xsl:value-of  select="MOI:ApplicationForIssuanceOfPersonalDocumentsOfBulgarianCitizensData/APIDBS:PermanentAddress/ADR:StreetText/."/>,&#160;
														№ <xsl:value-of  select="MOI:ApplicationForIssuanceOfPersonalDocumentsOfBulgarianCitizensData/APIDBS:PermanentAddress/ADR:BuildingNumber/."/>,&#160;
														вх. <xsl:value-of  select="MOI:ApplicationForIssuanceOfPersonalDocumentsOfBulgarianCitizensData/APIDBS:PermanentAddress/ADR:Entrance/."/>,&#160;
														ет. <xsl:value-of  select="MOI:ApplicationForIssuanceOfPersonalDocumentsOfBulgarianCitizensData/APIDBS:PermanentAddress/ADR:Floor/."/>,&#160;
														ап. <xsl:value-of  select="MOI:ApplicationForIssuanceOfPersonalDocumentsOfBulgarianCitizensData/APIDBS:PermanentAddress/ADR:Apartment/."/>
													</td>
												</tr>
												<tr>
													<td style="width: 95%; text-align: center;">
														Постоянен адрес (област, община, населено място, бул/ул., номер, вход, етаж, апартамент)
													</td>
												</tr>
												<tr>
													<td style="text-align: center; width: 90%;" class="dottedLine">
														област <xsl:value-of  select="MOI:ApplicationForIssuanceOfPersonalDocumentsOfBulgarianCitizensData/APIDBS:ChangedAddress/ADR:DistrictGRAOName/."/>,&#160;
														община <xsl:value-of  select="MOI:ApplicationForIssuanceOfPersonalDocumentsOfBulgarianCitizensData/APIDBS:ChangedAddress/ADR:MunicipalityGRAOName/."/>,&#160;
														населено място <xsl:value-of  select="MOI:ApplicationForIssuanceOfPersonalDocumentsOfBulgarianCitizensData/APIDBS:ChangedAddress/ADR:SettlementGRAOName/."/>,&#160;
														ул. <xsl:value-of  select="MOI:ApplicationForIssuanceOfPersonalDocumentsOfBulgarianCitizensData/APIDBS:ChangedAddress/ADR:StreetText/."/>,&#160;
														№ <xsl:value-of  select="MOI:ApplicationForIssuanceOfPersonalDocumentsOfBulgarianCitizensData/APIDBS:ChangedAddress/ADR:BuildingNumber/."/>,&#160;
														вх. <xsl:value-of  select="MOI:ApplicationForIssuanceOfPersonalDocumentsOfBulgarianCitizensData/APIDBS:ChangedAddress/ADR:Entrance/."/>,&#160;
														ет. <xsl:value-of  select="MOI:ApplicationForIssuanceOfPersonalDocumentsOfBulgarianCitizensData/APIDBS:ChangedAddress/ADR:Floor/."/>,&#160;
														ап. <xsl:value-of  select="MOI:ApplicationForIssuanceOfPersonalDocumentsOfBulgarianCitizensData/APIDBS:ChangedAddress/ADR:Apartment/."/>
													</td>
												</tr>
												<tr>
													<td style="width: 95%; text-align: center;">
														Променен постоянен адрес
													</td>
												</tr>
											</table>
										</td>
									</tr>
									<tr>
										<td align="center" colspan="4">
											<table style="width: 100%;font-size:9px;">
												<tr>
													<td>
														Пол
														<xsl:choose>
															<xsl:when test="MOI:ApplicationForIssuanceOfPersonalDocumentsOfBulgarianCitizensData/APIDBS:Person/PI:PersonIdentification/ID:Gender/GENDER:Genders/GENDER:Gender/GENDER:Code ='MALE' or MOI:ApplicationForIssuanceOfPersonalDocumentsOfBulgarianCitizensData/APIDBS:Person/PI:PersonIdentification/ID:Gender/GENDER:Genders/GENDER:Gender/GENDER:Code ='Male'">
																<b>М</b>
															</xsl:when>
															<xsl:otherwise>
																<b>Ж</b>
															</xsl:otherwise>
														</xsl:choose>

													</td>
													<td>
														| Ръст <xsl:value-of  select="MOI:ApplicationForIssuanceOfPersonalDocumentsOfBulgarianCitizensData/APIDBS:Person/PI:Height/."/>
													</td>
													<td>
														| Друго гражданство&#160;<xsl:value-of  select="MOI:ApplicationForIssuanceOfPersonalDocumentsOfBulgarianCitizensData/APIDBS:Citizenship/CH:CountryName/."/>
													</td>
													<td>
														| Указ
													</td>
												</tr>
											</table>
										</td>
									</tr>
									<tr>
										<td align="center" colspan="4">
											<table width="100%" style="font-size:9px;">
												<tr>
													<td>
														Цвят на очите:
													</td>
													<td>
														черни
														<xsl:choose>
															<xsl:when test="MOI:ApplicationForIssuanceOfPersonalDocumentsOfBulgarianCitizensData/APIDBS:Person/PI:EyesColor='3292'">
																<input type="checkbox" checked="true" disabled="" readonly="" />
															</xsl:when>
															<xsl:otherwise>
																<input type="checkbox"  disabled="" readonly="" />
															</xsl:otherwise>
														</xsl:choose>
													</td>

													<td>
														кафяви
														<xsl:choose>
															<xsl:when test="MOI:ApplicationForIssuanceOfPersonalDocumentsOfBulgarianCitizensData/APIDBS:Person/PI:EyesColor='1288'">
																<input type="checkbox" checked="true" disabled="" readonly="" />
															</xsl:when>
															<xsl:otherwise>
																<input type="checkbox"  disabled="" readonly="" />
															</xsl:otherwise>
														</xsl:choose>
													</td>
													<td>
														сини
														<xsl:choose>
															<xsl:when test="MOI:ApplicationForIssuanceOfPersonalDocumentsOfBulgarianCitizensData/APIDBS:Person/PI:EyesColor='2698'">
																<input type="checkbox" checked="true" disabled="" readonly="" />
															</xsl:when>
															<xsl:otherwise>
																<input type="checkbox"  disabled="" readonly="" />
															</xsl:otherwise>
														</xsl:choose>
													</td>
													<td>
														сиви
														<xsl:choose>
															<xsl:when test="MOI:ApplicationForIssuanceOfPersonalDocumentsOfBulgarianCitizensData/APIDBS:Person/PI:EyesColor='2704'">
																<input type="checkbox" checked="true" disabled="" readonly="" />
															</xsl:when>
															<xsl:otherwise>
																<input type="checkbox"  disabled="" readonly="" />
															</xsl:otherwise>
														</xsl:choose>
													</td>
													<td>
														зелени
														<xsl:choose>
															<xsl:when test="MOI:ApplicationForIssuanceOfPersonalDocumentsOfBulgarianCitizensData/APIDBS:Person/PI:EyesColor='3227'">
																<input type="checkbox" checked="true" disabled="" readonly="" />
															</xsl:when>
															<xsl:otherwise>
																<input type="checkbox"  disabled="" readonly="" />
															</xsl:otherwise>
														</xsl:choose>
													</td>
													<td>
														червени
														<xsl:choose>
															<xsl:when test="MOI:ApplicationForIssuanceOfPersonalDocumentsOfBulgarianCitizensData/APIDBS:Person/PI:EyesColor='21773'">
																<input type="checkbox" checked="true" disabled="" readonly="" />
															</xsl:when>
															<xsl:otherwise>
																<input type="checkbox"  disabled="" readonly="" />
															</xsl:otherwise>
														</xsl:choose>
													</td>
													<td>
														пъстри
														<xsl:choose>
															<xsl:when test="MOI:ApplicationForIssuanceOfPersonalDocumentsOfBulgarianCitizensData/APIDBS:Person/PI:EyesColor='2472'">
																<input type="checkbox" checked="true" disabled="" readonly="" />
															</xsl:when>
															<xsl:otherwise>
																<input type="checkbox"  disabled="" readonly="" />
															</xsl:otherwise>
														</xsl:choose>
													</td>
													<td>
														различни
														<xsl:choose>
															<xsl:when test="MOI:ApplicationForIssuanceOfPersonalDocumentsOfBulgarianCitizensData/APIDBS:Person/PI:EyesColor='22073'">
																<input type="checkbox" checked="true" disabled="" readonly="" />
															</xsl:when>
															<xsl:otherwise>
																<input type="checkbox"  disabled="" readonly="" />
															</xsl:otherwise>
														</xsl:choose>
													</td>
												</tr>
											</table>
										</td>
									</tr>
									<tr>
										<td colspan="3">
											Телефон за връзка&#160;<xsl:value-of  select="MOI:ApplicationForIssuanceOfPersonalDocumentsOfBulgarianCitizensData/APIDBS:Phone/."/>
										</td>
										<td>
											<em>Попълва се в случай, че заявителят иска да бъде уведомен, когато документа е готов</em><br/>
											Ел. поща&#160;<xsl:if test="MOI:ServiceApplicantReceiptData/SARD:ServiceResultReceiptMethod='R-6001'">
												<xsl:value-of  select="MOI:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:EmailAddress"/>
											</xsl:if>
										</td>
									</tr>

									<tr>
										<td align="left" colspan="4">
											<b>В заявлението се съдържат автоматично попълнени данни от НБД "Население" чрез НАИФ "НРБЛД" актуални данни по гражданското състояние, както и снимка и подпис от последно издаден български личен документ</b>
										</td>
									</tr>
									<tr>
										<td align="center" colspan="4">
											<table width="100%" style="font-size:9px;">
												<tr>
													<td>
														Имена
													</td>

													<td>
														......
													</td>
												</tr>
											</table>
										</td>
									</tr>
									<tr>
										<td align="center" colspan="3">
											<table width="100%" style="font-size:9px;">
												<tr>
													<td>
														..........
													</td>

													<td>
														../../....г.
													</td>
													<td>
														............
													</td>
													<td>
														Дата ../../.... г.
													</td>

												</tr>
												<tr>
													<td>
														Лична карта №
													</td>

													<td>
														издадена на
													</td>

													<td colspan="2">
														от МВР
													</td>

												</tr>
											</table>
										</td>
										<td align="center">
											&#160;<br/>
											подпис на заявителя

										</td>
									</tr>
									<tr>
										<td align="center" colspan="3">
											<table width="100%" style="font-size:9px;">
												<tr>
													<td class="dottedLine">
														.................................
													</td>
												</tr>
												<tr>
													<td>
														Имена на служителя приел заявлението
													</td>
												</tr>
											</table>
										</td>
										<td align="center">
											&#160;<br/>подпис на служителя
										</td>
									</tr>
									<tr>
										<td align="left" colspan="4">
											<b>
												Долуподписаните родители на лица под 18 годишна възраст, настойници, попечители или други законни представители заявяваме издаването на документ за самоличност:
											</b>
										</td>
									</tr>
									<tr>
										<td align="center" colspan="3">
											<table width="100%" style="font-size:9px;">
												<tr>
													<td>
														Имена |
													</td>

													<td>
														.......................
													</td>
												</tr>
											</table>
										</td>
										<td align="center">
											ЕГН ............
										</td>
									</tr>
									<tr>
										<td align="center" colspan="3">
											<table width="100%" style="font-size:9px;">
												<tr>
													<td>
														.............
													</td>

													<td>
														../../....г.
													</td>
													<td>
														...............
													</td>
													<td>
														Дата ../../.... г.
													</td>

												</tr>
												<tr>
													<td>
														Лична карта №
													</td>

													<td>
														издадена на
													</td>

													<td colspan="2">
														от МВР
													</td>

												</tr>
											</table>
										</td>
										<td align="center">
											&#160;<br/>подпис
										</td>
									</tr>
									<tr>
										<td align="center" colspan="3">
											<table width="100%" style="font-size:9px;">
												<tr>
													<td>
														Имена |
													</td>

													<td>
														......
													</td>
												</tr>
											</table>
										</td>
										<td align="center">
											ЕГН .....
										</td>
									</tr>
									<tr>
										<td align="center" colspan="3">
											<table width="100%" style="font-size:9px;">
												<tr>
													<td>
														..........
													</td>

													<td>
														../../....г.
													</td>
													<td>
														............
													</td>
													<td>
														Дата ../../.... г.
													</td>

												</tr>
												<tr>
													<td>
														Лична карта №
													</td>

													<td>
														издадена на
													</td>

													<td colspan="2">
														от МВР
													</td>

												</tr>
											</table>
										</td>
										<td align="center">
											&#160;<br/>
											подпис

										</td>
									</tr>

									<tr>
										<td align="left" colspan="4">
											Попълва се само ако заявения/те документ/и за самоличност ще се получава/т от упълномощено лице.<br/>
											<b>Данни за упълномощено лице:</b>
										</td>
									</tr>
									<tr>
										<td align="center" colspan="3">
											<table width="100%" style="font-size:9px;">
												<tr>
													<td>
														Имена |
													</td>

													<td>
														......
													</td>
												</tr>
											</table>
										</td>
										<td align="center">
											ЕГН .....
										</td>
									</tr>
									<tr>
										<td align="center" colspan="3">
											<table width="100%" style="font-size:9px;">
												<tr>
													<td>
														..........
													</td>

													<td>
														../../....г.
													</td>
													<td>
														............
													</td>
													<td>
														Дата ../../.... г.
													</td>

												</tr>
												<tr>
													<td>
														Лична карта №
													</td>

													<td>
														издадена на
													</td>

													<td colspan="2">
														от МВР
													</td>

												</tr>
											</table>
										</td>
										<td align="center">
											&#160;<br/>
											подпис

										</td>
									</tr>
									<tr>
										<td align="center" colspan="3">
											<table width="100%" style="font-size:9px;">
												<tr>
													<td>
														<em>Попълва само от лица, живеещи предимно в чужбина при подаване на заявление за издаване на лична карта.</em>
													</td>
												</tr>
												<tr>
													<td>
														<xsl:value-of select="MOI:ApplicationForIssuanceOfPersonalDocumentsOfBulgarianCitizensData/APIDBS:AbroadAddress/." />
													</td>
												</tr>
												<tr>
													<td>
														Адрес на територията на държавата, където пребивават
													</td>
												</tr>
											</table>
										</td>
										<td align="center">
											С попълването на това поле не се заявява настоящ адрес.
										</td>
									</tr>

									<tr>
										<td align="center" colspan="3">
											<table width="100%" style="font-size:9px;">
												<tr>
													<td>
														Семейно положение:
													</td>
													<td>
														женен/омъжена
													</td>

													<td>
														разведен/а
													</td>
													<td>
														вдовец/вдовица
													</td>
													<td>
														неженен/неомъжена
													</td>
												</tr>
												<tr>
													<td colspan="5">
														Съпруг/а: <xsl:value-of select="MOI:ApplicationForIssuanceOfPersonalDocumentsOfBulgarianCitizensData/APIDBS:SpouseData/SPOUSE:PersonBasicData/SPDATA:Names/SPNM:First /." />&#160;
														<xsl:value-of select="MOI:ApplicationForIssuanceOfPersonalDocumentsOfBulgarianCitizensData/APIDBS:SpouseData/SPOUSE:PersonBasicData/SPDATA:Names/SPNM:Middle/." />&#160;
														<xsl:value-of select="MOI:ApplicationForIssuanceOfPersonalDocumentsOfBulgarianCitizensData/APIDBS:SpouseData/SPOUSE:PersonBasicData/SPDATA:Names/SPNM:Last/." />&#160;
													</td>
												</tr>
											</table>
										</td>
										<td align="center" colspan="3">
											ЕГН: <xsl:value-of select="MOI:ApplicationForIssuanceOfPersonalDocumentsOfBulgarianCitizensData/APIDBS:SpouseData/SPOUSE:PersonBasicData/SPDATA:Identifier/." />
										</td>
									</tr>
									<tr>
										<td>
											Родители
										</td>

										<td colspan="2">
											Имена
										</td>

										<td>
											Дата на раждане
										</td>

									</tr>

									<tr>
										<td>
											Майка
										</td>

										<td colspan="2">
											<xsl:for-each select="MOI:ApplicationForIssuanceOfPersonalDocumentsOfBulgarianCitizensData/APIDBS:MotherData">
												<xsl:value-of select="CRBD:CitizenshipRegistrationBasicData/SPOUSE:PersonBasicData/SPDATA:Names/SPNM:First/." />&#160;
												<xsl:value-of select="CRBD:CitizenshipRegistrationBasicData/SPOUSE:PersonBasicData/SPDATA:Names/SPNM:Middle/." />&#160;
												<xsl:value-of select="CRBD:CitizenshipRegistrationBasicData/SPOUSE:PersonBasicData/SPDATA:Names/SPNM:Last/." />&#160;
											</xsl:for-each>
										</td>

										<td>
											<xsl:value-of select="ms:format-date(MOI:ApplicationForIssuanceOfPersonalDocumentsOfBulgarianCitizensData/APIDBS:MotherData/CRBD:CitizenshipRegistrationBasicData/SPOUSE:BirthDate/. , 'dd.MM.yyyy') "/> г.
										</td>

									</tr>

									<tr>
										<td>
											Баща
										</td>

										<td colspan="2">
											<xsl:for-each select="MOI:ApplicationForIssuanceOfPersonalDocumentsOfBulgarianCitizensData/APIDBS:FatherData">
												<xsl:value-of select="CRBD:CitizenshipRegistrationBasicData/SPOUSE:PersonBasicData/SPDATA:Names/SPNM:First/." />&#160;
												<xsl:value-of select="CRBD:CitizenshipRegistrationBasicData/SPOUSE:PersonBasicData/SPDATA:Names/SPNM:Middle/." />&#160;
												<xsl:value-of select="CRBD:CitizenshipRegistrationBasicData/SPOUSE:PersonBasicData/SPDATA:Names/SPNM:Last/." />&#160;
											</xsl:for-each>
										</td>

										<td>
											<xsl:value-of select="ms:format-date(MOI:ApplicationForIssuanceOfPersonalDocumentsOfBulgarianCitizensData/APIDBS:FatherData/CRBD:CitizenshipRegistrationBasicData/SPOUSE:BirthDate/. , 'dd.MM.yyyy') "/> г.
										</td>

									</tr>
								</table>
							</td>
						</tr>

						<xsl:choose>
							<xsl:when test = "MOI:Declarations">
								<xsl:for-each select="MOI:Declarations/MOI:Declaration">
									<xsl:choose>
										<xsl:when test="DECL:IsDeclarationFilled = 'true'">
											<tr>
												<td colspan="3">
													<xsl:value-of  select="DECL:DeclarationName" disable-output-escaping="yes"/>
												</td>
											</tr>
											<xsl:choose>
												<xsl:when test="DECL:FurtherDescriptionFromDeclarer">
													<tr>
														<td colspan="3">
															Декларирам (допълнително описание на обстоятелствата по декларацията):<xsl:value-of  select="DECL:FurtherDescriptionFromDeclarer"/>
														</td>
													</tr>
												</xsl:when>
											</xsl:choose>
										</xsl:when>
									</xsl:choose>
								</xsl:for-each>
							</xsl:when>
						</xsl:choose>

						<tr>
							<td width="50%">
								Дата:&#160;<xsl:value-of  select="ms:format-date(MOI:ElectronicAdministrativeServiceFooter/EASF:ApplicationSigningTime , 'dd.MM.yyyy')"/>г.
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