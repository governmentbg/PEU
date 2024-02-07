﻿import { EAUBaseDataService } from "eau-core";
import { UnitInfo, ServiceTermType } from "../models/ModelsAutoGenerated";
import { AISKATNomenclatureItem } from "../models/ModelsManualAdded";

export class NomenclaturesDataService extends EAUBaseDataService {

    protected baseUrl(): string {

        return super.baseUrl() + "Nomenclatures";
    }

    public getDeliveryRootUnitsInfo(serviceID: number, termType?: ServiceTermType): Promise<UnitInfo[]> {
        if (termType) {
            return this.get<UnitInfo[]>(`DeliveryRootUnitsInfo/${serviceID}/${+termType}`, UnitInfo);
        }

        return this.get<UnitInfo[]>(`DeliveryRootUnitsInfo/${serviceID}`, UnitInfo);
    }

    public getDeliveryUnitsInfo(serviceID: number): Promise<UnitInfo[]> {
        return this.get<UnitInfo[]>(`DeliveryUnitsInfo/${serviceID}`, UnitInfo);
    }

    public getDeliveryUnitsInfoNew(serviceID: number, termType: ServiceTermType): Promise<UnitInfo[]> {
        return this.get<UnitInfo[]>(`DeliveryUnitsInfo/${serviceID}/${+termType}`, UnitInfo);
    }

    public getServingUnitsInfo(serviceID: number): Promise<UnitInfo[]> {
        return this.get<UnitInfo[]>(`ServingUnitsInfo/${serviceID}`, UnitInfo);
    }

    public getTerminationOfRegistrationOfVehicle(): Promise<AISKATNomenclatureItem[]> {
        return this.get<AISKATNomenclatureItem[]>('КАТ/TerminationOfRegistrationOfVehicle', AISKATNomenclatureItem);
    }

    public getReasonForIssuingDuplicateOfSRMPS(): Promise<AISKATNomenclatureItem[]> {
        return this.get<AISKATNomenclatureItem[]>('КАТ/ReasonForIssuingDuplicateOfSRMPS', AISKATNomenclatureItem);
    }

    public getCountries(): Promise<AISKATNomenclatureItem[]> {
        return this.get<AISKATNomenclatureItem[]>('KAT/Countries', AISKATNomenclatureItem);
    }

    public getProvinceCodes(policeDepartmentID: number, vehicleTypeCode: number): Promise<string[]> {
        return this.get<string[]>('KAT/RegistrationNumberProvinceCodes', 'string', { policeDepartmentID: policeDepartmentID, vehicleTypeCode: vehicleTypeCode });
    }

    public getVehicleBaseColors(): Promise<AISKATNomenclatureItem[]> {
        return this.get<AISKATNomenclatureItem[]>('KAT/VehicleBaseColors', AISKATNomenclatureItem);
    }
}