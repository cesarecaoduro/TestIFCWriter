using GeometryGym.Ifc;

var db = new DatabaseIfc(ReleaseVersion.IFC4X3_RC4);
var site = new IfcSite(db, "MySite")
{
    Description = "Just another site",
};



var bridge = new IfcBridge(site, null, null)
{
    Name = "MyBridge",
    Description = "Just another bridge"
};

var project = new IfcProject(site, "MyProject", IfcUnitAssignment.Length.Metre);
{
};


var foundations = new IfcFacilityPart(
    bridge,
    "Foundations",
    new IfcFacilityPartTypeSelect(IfcFacilityPartCommonTypeEnum.BELOWGROUND),
    IfcFacilityUsageEnum.NOTDEFINED
    );

var name = "Pile Cap";
var length = 1.2;
var width = 1.2;
var depth = 2.4;

IfcMaterial material = new IfcMaterial(db, "Concrete")
{
};

IfcFootingType footingType = new IfcFootingType(db, name, IfcFootingTypeEnum.PAD_FOOTING)
{
    MaterialSelect = material,
};

IfcRectangleProfileDef rect = new IfcRectangleProfileDef(db, name, length, width);
IfcExtrudedAreaSolid extrusion = new IfcExtrudedAreaSolid(rect, new IfcAxis2Placement3D(new IfcCartesianPoint(db, 0, 0, 0)), new IfcDirection(db, 0, 0, 1), depth);

IfcProductDefinitionShape productRep = new IfcProductDefinitionShape(new IfcShapeRepresentation(extrusion));
IfcShapeRepresentation shapeRep = new(extrusion);

footingType.RepresentationMaps.Add(
    new IfcRepresentationMap(
        db.Factory.XYPlanePlacement,
        shapeRep
    )
);

IfcFooting footing = new(
    foundations,
    null,
    productRep
    )
{
    PredefinedType = IfcFootingTypeEnum.PAD_FOOTING,
    ObjectType = name
};

db.WriteFile(Path.Combine("IFC4X3RC4_testBridge.ifc"));
