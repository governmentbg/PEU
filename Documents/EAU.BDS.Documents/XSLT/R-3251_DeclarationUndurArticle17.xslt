<xsl:stylesheet version="1.0" xmlns:D="http://ereg.egov.bg/segment/R-3251"
						xmlns:EASH="http://ereg.egov.bg/segment/0009-000152"
						xmlns:ESA="http://ereg.egov.bg/segment/0009-000016"
						xmlns:AUT="http://ereg.egov.bg/segment/0009-000012"
						xmlns:P="http://ereg.egov.bg/segment/0009-000008"
						xmlns:REC="http://ereg.egov.bg/segment/0009-000015"
						xmlns:ID="http://ereg.egov.bg/segment/0009-000099"
						xmlns:IDT="http://ereg.egov.bg/nomenclature/0007-000016"
						xmlns:C="http://ereg.egov.bg/segment/R-3252"
						xmlns:ADR="http://ereg.egov.bg/segment/0009-000094"
						xmlns:RD="http://ereg.egov.bg/segment/R-3253"
				        xmlns:NM="http://ereg.egov.bg/segment/0009-000005"
						xmlns:ADD="http://ereg.egov.bg/segment/0009-000139"
						xmlns:EASF="http://ereg.egov.bg/segment/0009-000153"
                xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
                xmlns:xslExtension="urn:XSLExtension"
                xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
				xmlns:ms="urn:schemas-microsoft-com:xslt" xsi:type="xsl:transform" >

	<xsl:include href="./BDSBaseTemplates.xslt"/>
	<xsl:param name="SignatureXML"></xsl:param>

	<xsl:output omit-xml-declaration="yes" method="html"/>
	<xsl:template match="D:DeclarationUndurArticle17">
		<html>
			<xsl:call-template name="HeadNew">
				<xsl:with-param name="title">Декларация по чл. 17, ал. 1 от Правилника за издаване на българските лични документи - Портал за електронни административни услуги на МВР</xsl:with-param>
			</xsl:call-template>
			<body>
				<div class="print-document flex-container">
					<div class="document-section document-header">
						<h1 class="text-center document-title">ДЕКЛАРАЦИЯ</h1>
						<p class="text-center document-subtitle">по чл. 17, ал. 1 от Правилника за издаване на българските лични документи</p>
					</div>
					<div class="document-section">
						<p>
							Долуподписаният/ата&#160;<xsl:value-of  select="D:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Author/AUT:Person/P:Names/NM:First/."/>&#160;
							<xsl:if test="D:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Author/AUT:Person/P:Names/NM:Middle">
								<xsl:value-of  select="D:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Author/AUT:Person/P:Names/NM:Middle/."/>&#160;
							</xsl:if>
							<xsl:value-of  select="D:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Author/AUT:Person/P:Names/NM:Last/."/>
						</p>
						<p>
							ЕГН/ЛНЧ/ЛН&#160;<xsl:value-of  select="D:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Author/AUT:Person/P:Identifier/."/>&#160;с&#160;
							<xsl:choose>
								<xsl:when test="D:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Author/AUT:Person/P:IdentityDocument/ID:IdentityDocumentType = '0006-000087'">лична карта</xsl:when>
								<xsl:when test="D:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Author/AUT:Person/P:IdentityDocument/ID:IdentityDocumentType = '0006-000088'">паспорт</xsl:when>
								<xsl:when test="D:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Author/AUT:Person/P:IdentityDocument/ID:IdentityDocumentType = '0006-000089'">Дипломатически паспорт</xsl:when>
								<xsl:when test="D:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Author/AUT:Person/P:IdentityDocument/ID:IdentityDocumentType = '0006-000090'">Служебен паспорт</xsl:when>
								<xsl:when test="D:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Author/AUT:Person/P:IdentityDocument/ID:IdentityDocumentType = '0006-000091'">Моряшки паспорт</xsl:when>
								<xsl:when test="D:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Author/AUT:Person/P:IdentityDocument/ID:IdentityDocumentType = '0006-000092'">Военна карта за самоличност</xsl:when>
								<xsl:when test="D:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Author/AUT:Person/P:IdentityDocument/ID:IdentityDocumentType = '0006-000093'">Свидетелство за управление на моторно превозно средство</xsl:when>
								<xsl:when test="D:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Author/AUT:Person/P:IdentityDocument/ID:IdentityDocumentType = '0006-000094'">Временен паспорт</xsl:when>
								<xsl:when test="D:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Author/AUT:Person/P:IdentityDocument/ID:IdentityDocumentType = '0006-000095'">Служебен открит лист за преминаване на границата</xsl:when>
								<xsl:when test="D:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Author/AUT:Person/P:IdentityDocument/ID:IdentityDocumentType = '0006-000096'">Временен паспорт за окончателно напускане на Република България</xsl:when>								
								<xsl:when test="D:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Author/AUT:Person/P:IdentityDocument/ID:IdentityDocumentType = '0006-000097'">Карта на бежанец</xsl:when>
								<xsl:when test="D:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Author/AUT:Person/P:IdentityDocument/ID:IdentityDocumentType = '0006-000098'">Карта на чужденец, получил убежище</xsl:when>
								<xsl:when test="D:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Author/AUT:Person/P:IdentityDocument/ID:IdentityDocumentType = '0006-000099'">Карта на чужденец с хуманитарен статут</xsl:when>
								<xsl:when test="D:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Author/AUT:Person/P:IdentityDocument/ID:IdentityDocumentType = '0006-000121'">Разрешение за пребиваване</xsl:when>
								<xsl:when test="D:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Author/AUT:Person/P:IdentityDocument/ID:IdentityDocumentType = '0006-000122'">Удостоверение за пребиваване на гражданин на ЕС</xsl:when>
								<xsl:otherwise></xsl:otherwise>
							</xsl:choose>&#160;№&#160;<xsl:value-of  select="D:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Author/AUT:Person/P:IdentityDocument/ID:IdentityNumber/."/><br/>
							издаден/а от&#160;<xsl:value-of  select="D:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Author/AUT:Person/P:IdentityDocument/ID:IdentityIssuer/."/>&#160;на&#160;<xsl:value-of disable-output-escaping="yes" select="xslExtension:FormatDate(D:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Author/AUT:Person/P:IdentityDocument/ID:IdentitityIssueDate/., 'dd.MM.yyyy')"/>
						</p>
						<p>
							Постоянен (настоящ) адрес:&#160;
							<xsl:choose>
								<xsl:when test="((D:DeclarationUndurArticle17Data/C:PermanentAddress != null) and (D:DeclarationUndurArticle17Data/C:PermanentAddress/ADR:DistrictGRAOName/. != ''))">
									област <xsl:value-of  select="D:DeclarationUndurArticle17Data/C:PermanentAddress/ADR:DistrictGRAOName/."/>,&#160;
									община <xsl:value-of  select="D:DeclarationUndurArticle17Data/C:PermanentAddress/ADR:MunicipalityGRAOName/."/>,&#160;
									населено място <xsl:value-of  select="D:DeclarationUndurArticle17Data/C:PermanentAddress/ADR:SettlementGRAOName/."/>,&#160;
									ул. <xsl:value-of  select="D:DeclarationUndurArticle17Data/C:PermanentAddress/ADR:StreetText/."/>,&#160;
									№ <xsl:value-of  select="D:DeclarationUndurArticle17Data/C:PermanentAddress/ADR:BuildingNumber/."/>,&#160;
									вх. <xsl:value-of  select="D:DeclarationUndurArticle17Data/C:PermanentAddress/ADR:Entrance/."/>,&#160;
									ет. <xsl:value-of  select="D:DeclarationUndurArticle17Data/C:PermanentAddress/ADR:Floor/."/>,&#160;
									ап. <xsl:value-of  select="D:DeclarationUndurArticle17Data/C:PermanentAddress/ADR:Apartment/."/>
								</xsl:when>
								<xsl:otherwise>
									област <xsl:value-of  select="D:DeclarationUndurArticle17Data/C:PresentAddress/ADR:DistrictGRAOName/."/>,&#160;
									община <xsl:value-of  select="D:DeclarationUndurArticle17Data/C:PresentAddress/ADR:MunicipalityGRAOName/."/>,&#160;
									населено място <xsl:value-of  select="D:DeclarationUndurArticle17Data/C:PresentAddress/ADR:SettlementGRAOName/."/>,&#160;
									ул. <xsl:value-of  select="D:DeclarationUndurArticle17Data/C:PresentAddress/ADR:StreetText/."/>,&#160;
									№ <xsl:value-of  select="D:DeclarationUndurArticle17Data/C:PresentAddress/ADR:BuildingNumber/."/>,&#160;
									вх. <xsl:value-of  select="D:DeclarationUndurArticle17Data/C:PresentAddress/ADR:Entrance/."/>,&#160;
									ет. <xsl:value-of  select="D:DeclarationUndurArticle17Data/C:PresentAddress/ADR:Floor/."/>,&#160;
									ап. <xsl:value-of  select="D:DeclarationUndurArticle17Data/C:PresentAddress/ADR:Apartment/."/>
								</xsl:otherwise>
							</xsl:choose>
						</p>
					</div>
					<div class="document-section">
						<p style="text-align:center;">
							<span style="font-size: 1.125em;">
								<strong>ДЕКЛАРИРАМ:</strong>
							</span>
						</p>
						<p>
							<xsl:choose>
								<xsl:when test="D:DeclarationUndurArticle17Data/C:ReasonData/RD:Reason = '0'">Загуба</xsl:when>
								<xsl:when test="D:DeclarationUndurArticle17Data/C:ReasonData/RD:Reason = '1'">Кражба</xsl:when>
								<xsl:when test="D:DeclarationUndurArticle17Data/C:ReasonData/RD:Reason = '2'">Повреждане</xsl:when>
								<xsl:otherwise>Унищожаване</xsl:otherwise>
							</xsl:choose>&#160;на&#160;
							<xsl:choose>
								<xsl:when test="D:DeclarationUndurArticle17Data/C:DocType = '0'">лична карта</xsl:when>
								<xsl:when test="D:DeclarationUndurArticle17Data/C:DocType = '1'">паспорт</xsl:when>
								<xsl:otherwise>свидетелство за управление на МПС</xsl:otherwise>
							</xsl:choose>;<br/>
							Факти и обстоятелства (по загубването, открадването, повреждането или унищожаването на документа):&#160;<xsl:value-of  select="D:DeclarationUndurArticle17Data/C:ReasonData/RD:FactsAndCircumstances/."/>
						</p>
						<p>
							Известно ми е, че при намирането на българския личен документ съм длъжен/на незабавно да го предам на органите на Министерството на вътрешните работи, а в чужбина - на най-близкото дипломатическо или консулско представителство на Република България в чужбина.
						</p>
						<p>
							С подаването на декларацията съм уведомен/а, че&#160;
							<xsl:choose>
								<xsl:when test="D:DeclarationUndurArticle17Data/C:DocType = '0'">лична карта</xsl:when>
								<xsl:when test="D:DeclarationUndurArticle17Data/C:DocType = '1'">паспорт</xsl:when>
								<xsl:otherwise>свидетелство за управление на МПС</xsl:otherwise>
							</xsl:choose>&#160;ще бъде обявен/а за невалиден/на.
						</p>
						<p class="text-justify">
							За декларирани от мен неверни данни нося отговорност по чл. 313 от Наказателния кодекс.
						</p>
						<p class="text-justify">
							Запознат съм с прилаганата в МВР политика за поверителност, съгласно изискванията на Общия регламент относно защитата на данните (Регламент (ЕС) 2016/679 - GDPR).
						</p>
					</div>
					<div class="document-section">
						<h2 class="section-title">Приложени документи:</h2>
						<ol class="text-justify">
							<xsl:for-each select="D:AttachedDocuments/D:AttachedDocument">
								<li>
									<xsl:value-of select="ADD:AttachedDocumentDescription" />
								</li>
							</xsl:for-each>
						</ol>
					</div>
					<div class="document-section">
						<div class="flex-row">
							<div class="flex-col">
								<p>Дата:&#160;<xsl:value-of  select="ms:format-date(D:ElectronicAdministrativeServiceFooter/EASF:ApplicationSigningTime , 'dd.MM.yyyy')"/>&#160;г.</p>
							</div>
							<div class="flex-col">
								<xsl:call-template name="DocumentSignatures">
									<xsl:with-param name="Signatures" select = "$SignatureXML/DocumentSignatures" />
								</xsl:call-template>
							</div>
						</div>
					</div>
				</div>
			</body>
		</html>
	</xsl:template>
</xsl:stylesheet>
