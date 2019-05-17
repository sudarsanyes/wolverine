import { Component } from '@angular/core';
import { ProjectService } from '../Services/project.service';
import { Project, Group, Card } from '../Contracts/Contracts';

@Component({
  selector: 'sort',
  templateUrl: './sort.component.html'
})
export class SortComponent {
  ActiveProject: Project;

  constructor(private projectService: ProjectService) {
  }
}