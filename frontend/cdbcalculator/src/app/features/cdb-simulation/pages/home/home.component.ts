import { Component, DestroyRef, inject } from '@angular/core';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { CommonModule, CurrencyPipe } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpErrorResponse } from '@angular/common/http';
import { CdbSimulationService } from '../../../../core/services/cdb-simulation.service';
import { CdbSimulationRequest } from '../../models/cdb-simulation-request';
import { CdbSimulationResponse } from '../../models/cdb-simulation-response';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [CommonModule, FormsModule, CurrencyPipe],
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent {
  private readonly cdbSimulationService = inject(CdbSimulationService);
  private readonly destroyRef = inject(DestroyRef);

  initialAmount: number | null = null;
  months: number | null = null;

  grossAmount: number | null = null;
  netAmount: number | null = null;

  errorMessage: string | null = null;
  isLoading = false;

  simulate(): void {
    this.errorMessage = null;
    this.grossAmount = null;
    this.netAmount = null;

    if (this.initialAmount === null || this.initialAmount <= 0) {
      this.errorMessage = 'O valor inicial deve ser maior que zero.';
      return;
    }

    if (this.months === null || this.months <= 1) {
      this.errorMessage = 'O prazo deve ser maior que um mês.';
      return;
    }

    const request: CdbSimulationRequest = {
      initialAmount: this.initialAmount,
      months: this.months
    };

    this.isLoading = true;

    this.cdbSimulationService.simulate(request).pipe(
      takeUntilDestroyed(this.destroyRef)
    ).subscribe({
      next: (response: CdbSimulationResponse) => {
        this.grossAmount = response.grossAmount;
        this.netAmount = response.netAmount;
        this.isLoading = false;
      },
      error: (error: HttpErrorResponse) => {
        this.isLoading = false;

        if (error.error?.detail) {
          this.errorMessage = error.error.detail;
          return;
        }

        if (error.error?.title) {
          this.errorMessage = error.error.title;
          return;
        }

        this.errorMessage = 'Ocorreu um erro inesperado ao simular o investimento.';
      }
    });
  }

  reset(): void {
    this.initialAmount = null;
    this.months = null;
    this.grossAmount = null;
    this.netAmount = null;
    this.errorMessage = null;
    this.isLoading = false;
  }
}