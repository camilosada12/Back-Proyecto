import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-select-db',
  imports: [FormsModule],
  templateUrl: './select-db.component.html',
  styleUrl: './select-db.component.css'
})
export class SelectDbComponent implements OnInit{
  selectedDb: string = 'SqlServer';

  ngOnInit(): void {
    const savedDb = localStorage.getItem('dbProvider');
    this.selectedDb = savedDb || 'SqlServer';
  }

  onDbChange(): void {
    localStorage.setItem('dbProvider', this.selectedDb);
    location.reload(); // recarga para aplicar cambios en backend
  }
}
