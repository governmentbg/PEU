<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0"
				xmlns:N="http://ereg.egov.bg/segment/R-3260"
				xmlns:EASH="http://ereg.egov.bg/segment/0009-000152"
				xmlns:ND="http://ereg.egov.bg/segment/R-3261"
				xmlns:LI="http://ereg.egov.bg/segment/R-3262"
				xmlns:PD="http://ereg.egov.bg/segment/R-3037"
				xmlns:CCDI="http://ereg.egov.bg/segment/R-3267"
				xmlns:A="http://ereg.egov.bg/segment/R-3263"
				xmlns:P="http://ereg.egov.bg/segment/R-3264"
				xmlns:E="http://ereg.egov.bg/segment/R-3265"
				xmlns:F="http://ereg.egov.bg/segment/R-3266"
				xmlns:EASF="http://ereg.egov.bg/segment/0009-000153"
				xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
                xmlns:msxsl="urn:schemas-microsoft-com:xslt"
				xmlns:ms="urn:schemas-microsoft-com:xslt"
				xmlns:xslExtension="urn:XSLExtension"
				exclude-result-prefixes="msxsl">

	<xsl:include href="./KOSBaseTemplates.xslt"/>
	<xsl:param name="SignatureXML"></xsl:param>
	<xsl:output omit-xml-declaration="yes" method="html"/>

	<xsl:template match="N:NotificationForControlCoupon">
		<html>
			<xsl:call-template name="HeadNew">
				<xsl:with-param name="title">Уведомление за попълване на контролен талон - Портал за електронни административни услуги на МВР</xsl:with-param>
			</xsl:call-template>
			<body>
				<div class="print-document flex-container">
					<div class="document-section document-header">

						<div class="flex-row row-table">
							<div class="flex-col"></div>
							<div class="flex-col">
								<p class="text-bold text-uppercase">
									<xsl:call-template name="IssuingPoliceDepartmentHeader">
										<xsl:with-param name="IssuingPoliceDepartment" select = "N:NotificationForControlCouponData/ND:LicenseInfo/LI:IssuingPoliceDepartment" />
									</xsl:call-template>
								</p>
							</div>
						</div>

						<h1 class="text-center document-title">УВЕДОМЛЕНИЕ</h1>
					</div>

					<div class="document-section">
						<xsl:call-template name="ApplicationElectronicServiceApplicant">
							<xsl:with-param name="ElectronicServiceApplicant" select = "N:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant" />
						</xsl:call-template>
					</div>

					<div class="document-section">
						<p class="text-indent">Господин Началник,</p>
						<p class="text-indent text-justify">Предоставям данни за контролен талон към разрешение № <xsl:value-of select="N:NotificationForControlCouponData/ND:LicenseInfo/LI:PermitNumber/." />, Разрешение за придобиване на огнестрелни оръжия и боеприпаси за тях, Срок на валидност: <xsl:value-of select="ms:format-date(N:NotificationForControlCouponData/ND:LicenseInfo/LI:ValidityPeriodStart/., 'dd.MM.yyyy')" />&#160;–&#160;<xsl:value-of select="ms:format-date(N:NotificationForControlCouponData/ND:LicenseInfo/LI:ValidityPeriodEnd/., 'dd.MM.yyyy')" />, издадено на:&#160;<xsl:value-of select="N:NotificationForControlCouponData/ND:LicenseInfo/LI:HolderName/." />, ЕГН:&#160;<xsl:value-of select="N:NotificationForControlCouponData/ND:LicenseInfo/LI:HolderIdentifier/." />, издадено от:&#160;<xsl:value-of select="N:NotificationForControlCouponData/ND:LicenseInfo/LI:IssuingPoliceDepartment/PD:PoliceDepartmentName/." /></p>
					</div>

					<div class="document-section">
						<h2 class="section-title">Данни за контролен талон</h2>

						<xsl:if test="N:NotificationForControlCouponData/ND:ControlCouponData/ND:ControlCouponDataItem/CCDI:Firearms">
							<p>
								<b class="text-underline">Категория: Огнестрелно оръжие</b>
							</p>

							<table class="table table-borderless">
								<thead>
									<tr>
										<th></th>
										<th>Марка</th>
										<th>Модел</th>
										<th>Калибър</th>
										<th>Сериен №</th>
										<th>Вид</th>
									</tr>
								</thead>
								<tbody>
									<xsl:for-each select="N:NotificationForControlCouponData/ND:ControlCouponData/ND:ControlCouponDataItem/CCDI:Firearms">
										<tr>
											<td>
												<xsl:value-of select="position()" />.
											</td>
											<td>
												<xsl:value-of select="F:Brand" />
											</td>
											<td>
												<xsl:value-of select="F:Model" />
											</td>
											<td>
												<xsl:value-of select="F:Caliber" />
											</td>
											<td>
												<xsl:value-of select="F:SerialNumber" />
											</td>
											<td>
												<xsl:value-of select="F:KindName" />
											</td>
										</tr>
									</xsl:for-each>
								</tbody>
							</table>
						</xsl:if>

						<xsl:if test="N:NotificationForControlCouponData/ND:ControlCouponData/ND:ControlCouponDataItem/CCDI:Ammunition">
							<p>
								<b class="text-underline">Категория: Боеприпаси</b>
							</p>

							<table class="table table-borderless">
								<thead>
									<tr>
										<th></th>
										<th>Търговско наименование</th>
										<th>ООН номер</th>
										<th>Калибър</th>
										<th>Брой</th>
									</tr>
								</thead>
								<tbody>
									<xsl:for-each select="N:NotificationForControlCouponData/ND:ControlCouponData/ND:ControlCouponDataItem/CCDI:Ammunition">
										<tr>
											<td>
												<xsl:value-of select="position()" />.
											</td>
											<td>
												<xsl:value-of select="A:TradeName" />
											</td>
											<td>
												<xsl:value-of select="A:NumberOON" />
											</td>
											<td>
												<xsl:value-of select="A:Caliber" />
											</td>
											<td>
												<xsl:value-of select="A:Count" />
											</td>
										</tr>
									</xsl:for-each>
								</tbody>
							</table>
						</xsl:if>

						<xsl:if test="N:NotificationForControlCouponData/ND:ControlCouponData/ND:ControlCouponDataItem/CCDI:Explosives">
							<p>
								<b class="text-underline">Категория: Взривни вещества</b>
							</p>

							<table class="table table-borderless">
								<thead>
									<tr>
										<th></th>
										<th>Търговско наименование</th>
										<th>ООН номер</th>
										<th>Количество</th>
										<th>Мярка</th>
									</tr>
								</thead>
								<tbody>
									<xsl:for-each select="N:NotificationForControlCouponData/ND:ControlCouponData/ND:ControlCouponDataItem/CCDI:Explosives">
										<tr>
											<td>
												<xsl:value-of select="position()" />.
											</td>
											<td>
												<xsl:value-of select="E:TradeName" />
											</td>
											<td>
												<xsl:value-of select="E:NumberOON" />
											</td>
											<td>
												<xsl:value-of select="E:Quantity" />
											</td>
											<td>
												<xsl:value-of select="E:Measure" />
											</td>
										</tr>
									</xsl:for-each>
								</tbody>
							</table>
						</xsl:if>

						<xsl:if test="N:NotificationForControlCouponData/ND:ControlCouponData/ND:ControlCouponDataItem/CCDI:Pyrotechnics">
							<p>
								<b class="text-underline">Категория: Пиротехника</b>
							</p>

							<table class="table table-borderless">
								<thead>
									<tr>
										<th></th>
										<th>Търговско наименование</th>
										<th>Категория</th>
										<th>Количество</th>
										<th>Мярка</th>
									</tr>
								</thead>
								<tbody>
									<xsl:for-each select="N:NotificationForControlCouponData/ND:ControlCouponData/ND:ControlCouponDataItem/CCDI:Pyrotechnics">
										<tr>
											<td>
												<xsl:value-of select="position()" />.
											</td>
											<td>
												<xsl:value-of select="P:TradeName" />
											</td>
											<td>
												<xsl:value-of select="P:Kind" />
											</td>
											<td>
												<xsl:value-of select="P:Quantity" />
											</td>
											<td>
												<xsl:value-of select="P:Measure" />
											</td>
										</tr>
									</xsl:for-each>
								</tbody>
							</table>
						</xsl:if>
					</div>

					<div class="document-section">
						<p class="text-justify">
							Запознат съм с прилаганата в МВР политика за поверителност, съгласно изискванията на Общия регламент относно защитата на данните (Регламент (ЕС) 2016/679 - GDPR).
						</p>
						<p class="text-justify">
							Декларирам, че предоставената информация, включително приложените документи, е вярна. Известно ми е, че за неверни данни нося отговорност по чл. 313 от Наказателния кодекс.
						</p>
					</div>
					
					<div class="document-section">
						<div class="flex-row">
							<div class="flex-col">
								<p>
									Дата:&#160;<xsl:value-of  select="ms:format-date(N:ElectronicAdministrativeServiceFooter/EASF:ApplicationSigningTime , 'dd.MM.yyyy')"/>&#160;г.
								</p>
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
