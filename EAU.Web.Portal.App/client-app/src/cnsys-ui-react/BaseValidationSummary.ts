import { ArrayHelper, BaseDataModel, ObjectHelper } from 'cnsys-core';
import { observable } from "mobx";
import { BaseComponent, BaseProps } from './BaseComponent';

//**Базов калс за ValidationSummary, всички негови наследници трябва да са @observer*
export abstract class BaseValidationSummary<TProps extends BaseProps> extends BaseComponent<TProps, BaseDataModel> {

    @observable private static modelsWithValidationSummary: BaseDataModel[];
    private currentModel: BaseDataModel;

    constructor(props?: TProps, context?: any) {
        super(props, context);

        this.componentWillUnmount = this.componentWillUnmount.bind(this);
        this.componentDidUpdate = this.componentDidUpdate.bind(this);
        this.componentDidMount = this.componentDidMount.bind(this);
    }

    public abstract renderInternal(errors: string[]): JSX.Element;

    //#region React ComponentLifecycle

    render() {
        return this.renderInternal(this.getErrors(this.model));
    }
    
    componentWillUnmount(): void {
        ArrayHelper.removeElement(BaseValidationSummary.modelsWithValidationSummary, this.currentModel);
    }

    componentDidUpdate(prevProps: TProps, prevState: any, prevContext: any): void {
        this.AddModelToModelsWithValidationSummary();
    }

    componentDidMount(): void {
        this.AddModelToModelsWithValidationSummary();
    }

    //#endregion

    //#region Helpers

    protected getErrors(model: BaseDataModel): string[] {
        var errors: string[] = [];

        var modelErrors = model.getErrors();

        for (var modelError of modelErrors) {
            if (modelError.errors && modelError.errors.length > 0) {
                errors = errors.concat(modelError.errors.map(err => err.message));
            }
        }

        var obj: any = model;
        var modelsWithSummary = BaseValidationSummary.modelsWithValidationSummary;

        for (var propName in obj) {
            if (propName != "constructor") {

                if (BaseDataModel.isExtendedBy(obj[propName])) {
                    //Не връщаме грешките за моделите, които са в excludedModels
                    if (modelsWithSummary && modelsWithSummary.indexOf(obj[propName]) > -1) {
                        continue;
                    }

                    var subModelErrors = this.getErrors(obj[propName]);
                    errors = errors.concat(subModelErrors);
                }

                //Не използвам Array.isArray(that[propName]) защото mobx променя обекта така че тази проверка не работи
                if (obj[propName] && ObjectHelper.isArray(obj[propName])) {
                    for (var elem of obj[propName]) {
                        if (BaseDataModel.isExtendedBy(elem)) {
                            //Не връщаме грешките за моделите, които са в excludedModels
                            if (modelsWithSummary && modelsWithSummary.indexOf(elem) > -1) {
                                continue;
                            }

                            var subModelErrors = this.getErrors(elem);
                            errors = errors.concat(subModelErrors);
                        }
                    }
                }
            }
        }

        return errors;
    }

    private AddModelToModelsWithValidationSummary() {
        if (BaseValidationSummary.modelsWithValidationSummary == undefined)
            BaseValidationSummary.modelsWithValidationSummary = [];

        if (!this.currentModel || this.currentModel != this.model) {

            if (this.currentModel) {
                ArrayHelper.removeElement(BaseValidationSummary.modelsWithValidationSummary, this.currentModel)

                var index = BaseValidationSummary.modelsWithValidationSummary.indexOf(this.currentModel);

                if (index > -1) {
                    BaseValidationSummary.modelsWithValidationSummary.splice(index, 1);
                }
            }

            this.currentModel = this.model;

            if (BaseValidationSummary.modelsWithValidationSummary.indexOf(this.currentModel) < 0) {
                BaseValidationSummary.modelsWithValidationSummary.push(this.currentModel);
            }
        }
    }

    //#endregion
}