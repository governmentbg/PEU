<xsl:stylesheet version="1.0" xmlns:MOI="http://ereg.egov.bg/segment/R-3055"
                xmlns:ds="http://www.w3.org/2000/09/xmldsig#"
                xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
                xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
                xmlns:APIDBS="http://ereg.egov.bg/segment/R-3056"
				xmlns:PF="http://ereg.egov.bg/segment/R-2450"
				xmlns:ON="http://ereg.egov.bg/segment/R-2451"
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

	<xsl:include href="./KATBaseTemplatesNewDesign.xslt"/>
	<xsl:param name="SignatureXML"></xsl:param>

	<xsl:output omit-xml-declaration="yes" method="html"/>
	<xsl:template match="MOI:ApplicationForIssuingDriverLicense">
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
									<xsl:when test="MOI:ApplicationForIssuingDriverLicenseData/APIDBS:IdentificationPhotoAndSignature/IPAS:IdentificationSignature/.">
										<xsl:value-of select="MOI:ApplicationForIssuingDriverLicenseData/APIDBS:IdentificationPhotoAndSignature/IPAS:IdentificationSignature/." />
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
								ЗАЯВЛЕНИЕ<br/> за издаване на свидетелство за управление на МПС
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
										</td>
									</tr>
									<tr>
										<td align="left">
											Дата на раждане/Date of birth: <br/>
											<xsl:value-of select="ms:format-date(MOI:ApplicationForIssuingDriverLicenseData/APIDBS:Person/PI:PersonIdentification/ID:BirthDate/. , 'dd.MM.yyyy') "/> г.
										</td>

									</tr>
									<tr>
										<td>
											<xsl:value-of select="MOI:ApplicationForIssuingDriverLicenseData/APIDBS:Person/PI:PersonIdentification/ID:Identifier/."/> <br />ЕГН/ЛНЧ/ЛН
										</td>
										<td>
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
																обикновена <input type="checkbox" checked="true" disabled="" readonly="" /> бърза <input type="checkbox" disabled="" readonly="" /> 
															</xsl:when>
															<xsl:when test="MOI:ServiceTermType='0006-000084'">
																обикновена <input type="checkbox" disabled="" readonly="" /> бърза <input type="checkbox" checked="true" disabled="" readonly="" /> 
															</xsl:when>
															<xsl:otherwise>
																обикновена <input type="checkbox" disabled="" readonly="" /> бърза <input type="checkbox" disabled="" readonly="" />
															</xsl:otherwise>
														</xsl:choose>
													</td>
												</tr>
											</table>
										</td>
									</tr>
									<tr>
										<td colspan="2" align="center">
											&#160;<br/>&#160;<br/>
											<xsl:value-of select="MOI:ApplicationForIssuingDriverLicenseData/APIDBS:Person/PI:PlaceOfBirthOtherData/. " />
											&#160;<br/>&#160;<br/>&#160;&#160;&#160;Място на раждане: (Населено място, община, област или държава) Place of birth:
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
														<xsl:value-of  select="MOI:ApplicationForIssuingDriverLicenseData/APIDBS:Person/PI:PersonIdentification/ID:Names/NM:First/."/>
														&#160;
														<xsl:value-of  select="MOI:ApplicationForIssuingDriverLicenseData/APIDBS:Person/PI:PersonIdentification/ID:Names/NM:Middle/."/>
														&#160;
														<xsl:value-of  select="MOI:ApplicationForIssuingDriverLicenseData/APIDBS:Person/PI:PersonIdentification/ID:Names/NM:Last/."/>
													</td>
												</tr>
												<tr>
													<td style="width: 95%; text-align: center;">
														Имена на кирилица (име, презиме, фамилия)
													</td>
												</tr>
												<tr>
													<td style=" border: solid 1px black; text-align: center; width: 90%;">
														<xsl:value-of  select="MOI:ApplicationForIssuingDriverLicenseData/APIDBS:Person/PI:PersonIdentification/ID:NamesLatin/NMLN:First/."/>
														&#160;
														<xsl:value-of  select="MOI:ApplicationForIssuingDriverLicenseData/APIDBS:Person/PI:PersonIdentification/ID:NamesLatin/NMLN:Middle/."/>
														&#160;
														<xsl:value-of  select="MOI:ApplicationForIssuingDriverLicenseData/APIDBS:Person/PI:PersonIdentification/ID:NamesLatin/NMLN:Last/."/>
													</td>
												</tr>
												<tr>
													<td style="width: 95%; text-align: center;">
														Имена на латиница (име, презиме, фамилия)
													</td>
												</tr>
											</table>
										</td>
									</tr>
									<tr>
										<td align="center" colspan="4">
											<table width="100%" style="font-size: 9px;">
												<tr>
													<td style=" border: solid 1px black; text-align: center; width: 90%;">
														<xsl:value-of  select="MOI:ApplicationForIssuingDriverLicenseData/APIDBS:PersonFamily/."/>
													</td>
												</tr>
												<tr>
													<td style="width: 95%; text-align: center;">
														Фамилия/ Family name
													</td>
												</tr>
												<tr>
													<td style=" border: solid 1px black; text-align: center; width: 90%;">
														<xsl:value-of  select="MOI:ApplicationForIssuingDriverLicenseData/APIDBS:OtherNames/."/>
													</td>
												</tr>
												<tr>
													<td style="width: 95%; text-align: center;">
														Други имена/ Other/s Names
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
														област <xsl:value-of  select="MOI:ApplicationForIssuingDriverLicenseData/APIDBS:Address/ADR:DistrictGRAOName/."/>,&#160;
														община <xsl:value-of  select="MOI:ApplicationForIssuingDriverLicenseData/APIDBS:Address/ADR:MunicipalityGRAOName/."/>,&#160;
														населено място <xsl:value-of  select="MOI:ApplicationForIssuingDriverLicenseData/APIDBS:Address/ADR:SettlementGRAOName/."/>,&#160;
														ул. <xsl:value-of  select="MOI:ApplicationForIssuingDriverLicenseData/APIDBS:Address/ADR:StreetText/."/>,&#160;
														№ <xsl:value-of  select="MOI:ApplicationForIssuingDriverLicenseData/APIDBS:Address/ADR:BuildingNumber/."/>,&#160;
														вх. <xsl:value-of  select="MOI:ApplicationForIssuingDriverLicenseData/APIDBS:Address/ADR:Entrance/."/>,&#160;
														ет. <xsl:value-of  select="MOI:ApplicationForIssuingDriverLicenseData/APIDBS:Address/ADR:Floor/."/>,&#160;
														ап. <xsl:value-of  select="MOI:ApplicationForIssuingDriverLicenseData/APIDBS:Address/ADR:Apartment/."/>
													</td>
												</tr>
												<tr>
													<td style="width: 95%; text-align: center;">
														Адрес (област, община, населено място, бул./ул., номер, вход, етаж, апартамент)/ Address
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
														Пол/Sex:
														<xsl:choose>
															<xsl:when test="MOI:ApplicationForIssuingDriverLicenseData/APIDBS:Person/PI:PersonIdentification/ID:Gender/GENDER:Genders/GENDER:Gender/GENDER:Code ='MALE' or MOI:ApplicationForIssuingDriverLicenseData/APIDBS:Person/PI:PersonIdentification/ID:Gender/GENDER:Genders/GENDER:Gender/GENDER:Code ='Male'">
																<b>М</b>
															</xsl:when>
															<xsl:otherwise>
																<b>Ж</b>
															</xsl:otherwise>
														</xsl:choose>
													</td>
													<td>
														| Ръст <xsl:value-of  select="MOI:ApplicationForIssuingDriverLicenseData/APIDBS:Person/PI:Height/."/>
													</td>
													<td>
														| Друго гражданство/Nationality:&#160;<xsl:value-of  select="MOI:ApplicationForIssuingDriverLicenseData/APIDBS:ForeignCitizenship/CH:CountryName/."/>
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
															<xsl:when test="MOI:ApplicationForIssuingDriverLicenseData/APIDBS:Person/PI:EyesColor='3292'">
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
															<xsl:when test="MOI:ApplicationForIssuingDriverLicenseData/APIDBS:Person/PI:EyesColor='1288'">
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
															<xsl:when test="MOI:ApplicationForIssuingDriverLicenseData/APIDBS:Person/PI:EyesColor='2698'">
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
															<xsl:when test="MOI:ApplicationForIssuingDriverLicenseData/APIDBS:Person/PI:EyesColor='2704'">
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
															<xsl:when test="MOI:ApplicationForIssuingDriverLicenseData/APIDBS:Person/PI:EyesColor='3227'">
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
															<xsl:when test="MOI:ApplicationForIssuingDriverLicenseData/APIDBS:Person/PI:EyesColor='21773'">
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
															<xsl:when test="MOI:ApplicationForIssuingDriverLicenseData/APIDBS:Person/PI:EyesColor='2472'">
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
															<xsl:when test="MOI:ApplicationForIssuingDriverLicenseData/APIDBS:Person/PI:EyesColor='22073'">
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
										<td align="center" colspan="4">
											<table style="width: 100%;font-size:9px;">
												<tr>
													<td>
														Телефон за връзка/ Phone&#160;<xsl:value-of  select="MOI:ApplicationForIssuingDriverLicenseData/APIDBS:Phone/."/>
													</td>
													<td>
														| Ел. поща&#160;<xsl:if test="MOI:ServiceApplicantReceiptData/SARD:ServiceResultReceiptMethod='R-6001'">
															<xsl:value-of  select="MOI:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:EmailAddress"/>
														</xsl:if>
													</td>
												</tr>
											</table>
										</td>
									</tr>
									<tr>
										<td colspan="4">
											Място на връчване/ Place of receiving <xsl:value-of  select="MOI:ServiceApplicantReceiptData/SARD:UnitInAdministration/SARD:AdministrativeDepartmentName"/>
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
								</table>
							</td>
						</tr>
						<tr>
							<td align="left" colspan="4">
								<b>В заявлението се съдържат автоматично попълнени данни от НБД "Население" чрез НАИФ "НРБЛД" актуални данни по гражданското състояние, както и снимка и подпис от последно издаден български личен документ</b>
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
															<xsl:choose>
															  <xsl:when test="DECL:DeclarationCode = 'DECL_DECLINE_DRIVER_LICENCE_CATEGORIES'">
																I would like to dicline of category/ies<br/>
																Попълнете категориите от които желаете да се откажете/Fill in categories which you would like to decline to: <xsl:value-of  select="DECL:FurtherDescriptionFromDeclarer"/>
															  </xsl:when>
															  <xsl:otherwise>
																Декларирам (допълнително описание на обстоятелствата по декларацията):<xsl:value-of  select="DECL:FurtherDescriptionFromDeclarer"/>
															  </xsl:otherwise>
															</xsl:choose>
															
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
								<xsl:call-template name="DocumentSignatures">
									<xsl:with-param name="Signatures" select = "$SignatureXML/DocumentSignatures" />
								</xsl:call-template>
							</td>
						</tr>
					</tbody>
				</table>
			</body>
		</html>
	</xsl:template>

</xsl:stylesheet>