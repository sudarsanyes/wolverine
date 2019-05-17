import { Component } from '@angular/core';
import { ProjectService } from '../Services/project.service';
import { Project, Group, Card, Guid } from '../Contracts/Contracts';
import { Router, ActivatedRoute } from '@angular/router';

@Component({
    selector: 'create',
    templateUrl: './create.component.html'
})
export class CreateComponent {

    ActiveProject: Project;
    ID: string;
    NewCard: Card;
    NewGroup: Group;

    constructor(private projectService: ProjectService, private router: Router, private route: ActivatedRoute) {
        this.NewCard = Card.getDefault();
        this.NewGroup = Group.getDefault();
    }

    ngOnInit() {
        this.ID = this.route.snapshot.paramMap.get('name');
        this.projectService.load(this.ID).subscribe((project: Project) => {
            this.ActiveProject = project;
            console.log(this.ActiveProject);
        });
    }

    onEditCard(card: Card) {
    }

    onDeleteCard(card: Card) {
        var index = 0;
        for(let c of this.ActiveProject.unsortedGroup.cards) {
            if(c.id == card.id) {
                this.ActiveProject.unsortedGroup.cards.splice(index, 1);
                break;
            }
            index++;
        }
    }
    
    onAddCard() {
        this.ActiveProject.unsortedGroup.cards.push(this.NewCard);
        this.NewCard = Card.getDefault();
    }

    onEditGroup(group: Group) {
    }

    onDeleteGroup(group: Group) {
        var index = 0;
        for(let g of this.ActiveProject.defaultGroups) {
            if(g.id == group.id) {
                this.ActiveProject.defaultGroups.splice(index, 1);
                break;
            }
            index++;
        }
    }
    
    onAddGroup() {
        this.ActiveProject.defaultGroups.push(this.NewGroup);
        this.NewGroup = Group.getDefault();
    }

    onAddCardToGroup(group: Group) {
        console.log(group);
        group.cards.push(this.NewCard);
        this.NewCard = Card.getDefault();
    }

    onDeleteCardFromGroup(group: Group, card: Card) {
        var index = 0;
        for(let c of group.cards) {
            if(c.id == card.id) {
                group.cards.splice(index, 1);
                break;
            }
            index++;
        }
    }
}