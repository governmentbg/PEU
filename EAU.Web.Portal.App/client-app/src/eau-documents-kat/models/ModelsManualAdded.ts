import { BaseDataModel, TypeSystem } from 'cnsys-core';
import { observable } from 'mobx';
import { moduleContext } from '../ModuleContext';

export enum VehicleTypeCode {
    /** автомобилна регистрационна табела */
    _8403 = 8403,
    /** тракторна/ремаркетна регистрационна табела */
    _8404 = 8404,
    /**  мотоциклетна регистрационна табела */
    _8405 = 8405,
    /**мотопедна регистрационна табела */
    _8406 = 8406,
    /**  АВТОМОБИЛНА ТАБЕЛА ЗА ЕЛЕКТРИЧЕСКО ПС */
    _21770 = 21770,
    /** МОТОЦИКЛЕТНА ТАБЕЛА ЗА ЕЛЕКТРИЧЕСКО ПС */
    _21771 = 21771,
    /** МОТОПЕДНА ТАБЕЛА ЗА ЕЛЕКТРИЧЕСКО ПС */
    _21772 = 21772
}
TypeSystem.registerEnumInfo(VehicleTypeCode, 'VehicleTypeCode', moduleContext.moduleName);

export enum NumberFormat {
    /**  Огледални (abba) */
    abba = 1,
    /** Карета (aaaa) */
    aaaa = 2,
    /** Чифтове (abab, aabb) */
    abab_aabb = 3,
    /** Поредни (abcd) */
    abcd = 4
}
TypeSystem.registerEnumInfo(NumberFormat, 'NumberFormat', moduleContext.moduleName);

export enum PlateStatus {
    /** СВОБОДЕН */
    Available = 0,
    /** ЗАЯВЕН */
    Requested = 1,
    /** ЗАПАЗЕН */
    Reserved = 2,
    _3 = 3,
    _4 = 4
}
TypeSystem.registerEnumInfo(PlateStatus, 'PlateStatus', moduleContext.moduleName);

export enum FourDigitSearchTypes {
    /** По интервал */
    ByRegNumber = 0,
    /** По номер */
    ByInterval = 1
}
TypeSystem.registerEnumInfo(FourDigitSearchTypes, 'FourDigitSearchTypes', moduleContext.moduleName);


@TypeSystem.typeDecorator('FourDigitSearchCriteria', moduleContext.moduleName)
export class FourDigitSearchCriteria extends BaseDataModel {
    @observable private _policeDepartment: number = null;
    @observable private _vehicleTypeCode: VehicleTypeCode = null;
    @observable private _type1PlatesCount: number = null;
    @observable private _type2PlatesCount: number = null;
    @observable private _fourDigitSearchType: FourDigitSearchTypes = null;
    @observable private _numberFormat: NumberFormat = null;
    @observable private _plateStatus: PlateStatus = null;
    @observable private _fromRegNumber: string = null;
    @observable private _toRegNumber: string = null;
    @observable private _specificRegNumber: string = null;

    constructor(obj?: any) {
        super(obj)

        this.copyFrom(obj);
    }

    @TypeSystem.propertyDecorator('number')
    public set policeDepartment(val: number) {
        this._policeDepartment = val;
    }

    public get policeDepartment(): number {
        return this._policeDepartment;
    }

    @TypeSystem.propertyDecorator(VehicleTypeCode ? VehicleTypeCode : moduleContext.moduleName + '.' + 'VehicleTypeCode')
    public set vehicleTypeCode(val: VehicleTypeCode) {
        this._vehicleTypeCode = val;
    }

    public get vehicleTypeCode(): VehicleTypeCode {
        return this._vehicleTypeCode;
    }

    @TypeSystem.propertyDecorator('number')
    public set type1PlatesCount(val: number) {
        this._type1PlatesCount = val;
    }

    public get type1PlatesCount(): number {
        return this._type1PlatesCount;
    }

    @TypeSystem.propertyDecorator('number')
    public set type2PlatesCount(val: number) {
        this._type2PlatesCount = val;
    }

    public get type2PlatesCount(): number {
        return this._type2PlatesCount;
    }

    @TypeSystem.propertyDecorator(FourDigitSearchTypes ? FourDigitSearchTypes : moduleContext.moduleName + '.' + 'FourDigitSearchTypes')
    public set fourDigitSearchType(val: FourDigitSearchTypes) {
        this._fourDigitSearchType = val;
    }

    public get fourDigitSearchType(): FourDigitSearchTypes {
        return this._fourDigitSearchType;
    }

    @TypeSystem.propertyDecorator(NumberFormat ? NumberFormat : moduleContext.moduleName + '.' + 'NumberFormat')
    public set numberFormat(val: NumberFormat) {
        this._numberFormat = val;
    }

    public get numberFormat(): NumberFormat {
        return this._numberFormat;
    }

    @TypeSystem.propertyDecorator('string')
    public set fromRegNumber(val: string) {
        this._fromRegNumber = val;
    }

    public get fromRegNumber(): string {
        return this._fromRegNumber;
    }

    @TypeSystem.propertyDecorator('string')
    public set toRegNumber(val: string) {
        this._toRegNumber = val;
    }

    public get toRegNumber(): string {
        return this._toRegNumber;
    }

    @TypeSystem.propertyDecorator(PlateStatus ? PlateStatus : moduleContext.moduleName + '.' + 'PlateStatus')
    public set plateStatus(val: PlateStatus) {
        this._plateStatus = val;
    }

    public get plateStatus(): PlateStatus {
        return this._plateStatus;
    }

    @TypeSystem.propertyDecorator('string')
    public set specificRegNumber(val: string) {
        this._specificRegNumber = val;
    }

    public get specificRegNumber(): string {
        return this._specificRegNumber;
    }
}

export interface IFourDigitsSearchResult {
    exceedResultLimiteWarnning: string;
    result: IPlateStatusResult[];
}

export interface IPlateStatusResult {
    number: string;
    plateStatus?: number;
}

@TypeSystem.typeDecorator('SpecialNumberSearchCriteria', moduleContext.moduleName)
export class SpecialNumberSearchCriteria extends BaseDataModel {
    @observable private _vehicleTypeCode: VehicleTypeCode = null;
    @observable private _provinceCode: string = null;
    @observable private _number: string = null;

    constructor(obj?: any) {
        super(obj)

        this.copyFrom(obj);
    }

    @TypeSystem.propertyDecorator(VehicleTypeCode ? VehicleTypeCode : moduleContext.moduleName + '.' + 'VehicleTypeCode')
    public set vehicleTypeCode(val: VehicleTypeCode) {
        this._vehicleTypeCode = val;
    }

    public get vehicleTypeCode(): VehicleTypeCode {
        return this._vehicleTypeCode;
    }

    @TypeSystem.propertyDecorator('string')
    public set provinceCode(val: string) {
        this._provinceCode = val;
    }

    public get provinceCode(): string {
        return this._provinceCode;
    }

    @TypeSystem.propertyDecorator('string')
    public set number(val: string) {
        this._number = val;
    }

    public get number(): string {
        return this._number;
    }
}