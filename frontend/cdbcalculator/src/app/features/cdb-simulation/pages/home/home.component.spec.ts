import { ComponentFixture, TestBed } from '@angular/core/testing';
import { of, throwError } from 'rxjs';
import { HttpErrorResponse } from '@angular/common/http';
import { HomeComponent } from './home.component';
import { CdbSimulationService } from '../../../../core/services/cdb-simulation.service';

describe('HomeComponent', () => {
  let component: HomeComponent;
  let fixture: ComponentFixture<HomeComponent>;
  let mockService: jasmine.SpyObj<CdbSimulationService>;

  beforeEach(async () => {
    mockService = jasmine.createSpyObj('CdbSimulationService', ['simulate']);

    await TestBed.configureTestingModule({
      imports: [HomeComponent],
      providers: [{ provide: CdbSimulationService, useValue: mockService }]
    }).compileComponents();

    fixture = TestBed.createComponent(HomeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  describe('simulate() — validation', () => {
    it('should set errorMessage when initialAmount is null', () => {
      component.initialAmount = null;
      component.months = 6;

      component.simulate();

      expect(component.errorMessage).toBe('O valor inicial deve ser maior que zero.');
      expect(mockService.simulate).not.toHaveBeenCalled();
    });

    it('should set errorMessage when initialAmount is zero', () => {
      component.initialAmount = 0;
      component.months = 6;

      component.simulate();

      expect(component.errorMessage).toBe('O valor inicial deve ser maior que zero.');
      expect(mockService.simulate).not.toHaveBeenCalled();
    });

    it('should set errorMessage when initialAmount is negative', () => {
      component.initialAmount = -500;
      component.months = 6;

      component.simulate();

      expect(component.errorMessage).toBe('O valor inicial deve ser maior que zero.');
      expect(mockService.simulate).not.toHaveBeenCalled();
    });

    it('should set errorMessage when months is null', () => {
      component.initialAmount = 1000;
      component.months = null;

      component.simulate();

      expect(component.errorMessage).toBe('O prazo deve ser maior que um mês.');
      expect(mockService.simulate).not.toHaveBeenCalled();
    });

    it('should set errorMessage when months is 1', () => {
      component.initialAmount = 1000;
      component.months = 1;

      component.simulate();

      expect(component.errorMessage).toBe('O prazo deve ser maior que um mês.');
      expect(mockService.simulate).not.toHaveBeenCalled();
    });
  });

  describe('simulate() — success', () => {
    beforeEach(() => {
      mockService.simulate.and.returnValue(of({ grossAmount: 1100, netAmount: 1080 }));
      component.initialAmount = 1000;
      component.months = 6;
    });

    it('should call the service with the correct request', () => {
      component.simulate();

      expect(mockService.simulate).toHaveBeenCalledWith({ initialAmount: 1000, months: 6 });
    });

    it('should set grossAmount and netAmount from the response', () => {
      component.simulate();

      expect(component.grossAmount).toBe(1100);
      expect(component.netAmount).toBe(1080);
    });

    it('should set isLoading to false after receiving the response', () => {
      component.simulate();

      expect(component.isLoading).toBeFalse();
    });

    it('should clear any previous errorMessage before calling the service', () => {
      component.errorMessage = 'Previous error';

      component.simulate();

      expect(component.errorMessage).toBeNull();
    });

    it('should clear previous results before calling the service', () => {
      component.grossAmount = 999;
      component.netAmount = 888;

      component.simulate();

      expect(component.grossAmount).toBe(1100);
      expect(component.netAmount).toBe(1080);
    });
  });

  describe('simulate() — error', () => {
    beforeEach(() => {
      component.initialAmount = 1000;
      component.months = 6;
    });

    it('should set errorMessage from error.error.detail', () => {
      const error = new HttpErrorResponse({ error: { detail: 'Initial amount is invalid.' }, status: 400 });
      mockService.simulate.and.returnValue(throwError(() => error));

      component.simulate();

      expect(component.errorMessage).toBe('Initial amount is invalid.');
    });

    it('should set errorMessage from error.error.title when detail is absent', () => {
      const error = new HttpErrorResponse({ error: { title: 'Bad Request' }, status: 400 });
      mockService.simulate.and.returnValue(throwError(() => error));

      component.simulate();

      expect(component.errorMessage).toBe('Bad Request');
    });

    it('should set generic errorMessage when error has neither detail nor title', () => {
      const error = new HttpErrorResponse({ error: {}, status: 500 });
      mockService.simulate.and.returnValue(throwError(() => error));

      component.simulate();

      expect(component.errorMessage).toBe('Ocorreu um erro inesperado ao simular o investimento.');
    });

    it('should set isLoading to false after an error', () => {
      const error = new HttpErrorResponse({ error: {}, status: 500 });
      mockService.simulate.and.returnValue(throwError(() => error));

      component.simulate();

      expect(component.isLoading).toBeFalse();
    });
  });

  describe('reset()', () => {
    it('should clear all fields', () => {
      component.initialAmount = 1000;
      component.months = 6;
      component.grossAmount = 1100;
      component.netAmount = 1080;
      component.errorMessage = 'Some error';
      component.isLoading = true;

      component.reset();

      expect(component.initialAmount).toBeNull();
      expect(component.months).toBeNull();
      expect(component.grossAmount).toBeNull();
      expect(component.netAmount).toBeNull();
      expect(component.errorMessage).toBeNull();
      expect(component.isLoading).toBeFalse();
    });
  });
});
