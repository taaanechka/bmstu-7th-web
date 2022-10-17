from faker import Faker
from datetime import datetime
from dateutil.relativedelta import relativedelta
from random import randint
from random import uniform
from random import choice

from faker_vehicle import VehicleProvider
from numpy import append

MAX_N = 1000
faker = Faker()
faker.add_provider(VehicleProvider)

dir_name = "database_postgres/init/"

def generate_comings_departures():
    f_c = open(dir_name + 'comings.csv', 'w')
    f_d = open(dir_name + 'departures.csv', 'w')

    for i in range(MAX_N):
        userId = randint(1, MAX_N)
        comingDate = faker.date_this_year()
        line_c = "{0};{1}\n".format(
                                userId,
                                comingDate)
        f_c.write(line_c)

        if (i % 2):
            userId = randint(1, MAX_N)
            departureDate = faker.date_between(comingDate, datetime.now())
            line_d = "{0};{1}\n".format(
                                userId,
                                departureDate)
            f_d.write(line_d)

    f_c.close()
    f_d.close()


def generate_users():
    f = open(dir_name + 'users.csv', 'w')

    for i in range(MAX_N):
        if i == 0:
            login = 'Admin'
            userType = 3
        else:
            login = faker.unique.user_name()
            userType = randint(1, 3)

        lst_ns = faker.name().split()
        name = lst_ns[0]
        surname = lst_ns[1]
        
        password = faker.password(length=12, special_chars=False, upper_case=False)
        line = "{0};{1};{2};{3};{4}\n".format(
                                                name,
                                                surname,
                                                login,
                                                password,
                                                userType)
        f.write(line)

    f.close()



def generate_owners():
    f = open(dir_name + 'car_owners.csv', 'w')
    
    for i in range(MAX_N // 2):
        lst_ns = faker.name().split()
        name = lst_ns[0]
        surname = lst_ns[1]
        email = faker.unique.email()
        line = "{0};{1};{2}\n".format(
                                        name,
                                        surname,
                                        email)
        f.write(line)

    f.close()


def generate_cars():
    cars_list = [faker.vehicle_object() for _ in range(MAX_N)] # Example: {'Year': 200, 'Make': 'Lexus', 'Model': 'SC', 'Category': 'Convertible'}
    brands_list = []
    models_list = []
    categories_list = []

    # dict_brand_model_category

    for elem in cars_list:
        if elem['Make'] not in brands_list:
            brands_list.append(elem['Make'])
        if elem['Model'] not in models_list:
            models_list.append(elem['Model'])
        if elem['Category'] not in categories_list:
            categories_list.append(elem['Category'])
    
    # brands
    wheel = ['left', 'right']
    f = open(dir_name + "brands.csv", 'w')

    for i in range(len(brands_list)):
        line = "{0};{1};{2}\n".format(
                                        brands_list[i], #brands_list[randint(0, linesLen - 1)],
                                        faker.country_code(), #country()
                                        choice(wheel))
        
        f.write(line)

    f.close()

    # models
    f = open(dir_name + "models.csv", 'w')
    brandId_modelId_category = []

    for i in range(len(models_list)):
        model_name = models_list[i]
        for elem in cars_list:
            if elem['Model'] == model_name:
                brand = elem['Make']
                category = elem['Category']
                break
        for j in range(len(brands_list)):
            if brands_list[j] == brand:
                brand_id = j + 1
                break
        brandId_modelId_category.append([brand_id, i + 1, category])
        
        line = "{0};{1}\n".format(
                                    brand_id, 
                                    model_name)
        f.write(line)

    f.close()

    # equipments
    modelId_equipmentId = []

    gear_list = ['front-wheel', 'rear', 'four-wheel']
    roof_list = ['covered', 'glazed', 'reclining']
    f = open(dir_name + "equipments.csv", 'w')

    for i in range(MAX_N):
        elem = choice(brandId_modelId_category)
        category_name = elem[2]
        modelId_equipmentId.append([elem[1], i + 1])

        line = "{0};{1};{2}\n".format(
                                        category_name, 
                                        choice(gear_list),
                                        choice(roof_list))
        f.write(line)

    f.close()

    # colors
    f = open(dir_name + "colors.csv", 'w')

    for i in range(MAX_N // 10):
        line = "{0}\n".format(faker.unique.color_name())
        f.write(line)

    f.close()

    #cars
    car_numbers = []
    f = open(dir_name + "cars.csv", 'w')

    for i in range(MAX_N):
        elem = choice(modelId_equipmentId)
        modelId = elem[0]
        equipmentId = elem[1]

        car_number = faker.unique.license_plate()

        car_numbers.append(car_number)

        line = "{0};{1};{2};{3};{4}\n".format(
                                                car_number,
                                                modelId, 
                                                equipmentId,
                                                randint(1, MAX_N // 10),
                                                i + 1)
        f.write(line)

    f.close()

    return car_numbers


def generate_links(car_numbers):
    f = open(dir_name + "links_owner_car_departure.csv", 'w')

    N = MAX_N // 2
    indexes = [i for i in range(1, MAX_N, 2)]

    for i in range(N):
        line = "{0};{1};{2}\n".format(
                                        randint(1, N),              #owner_id
                                        car_numbers[indexes[i]],    #car_number
                                        i + 1)                      #departure_id
        f.write(line)

    f.close()

if __name__ == "__main__":
    # generate_comings_departures()
    # generate_users()
    # generate_owners()

    car_numbers = generate_cars()
    generate_links(car_numbers)